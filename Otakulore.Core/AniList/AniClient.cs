using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class AniClient
{

    private readonly GraphQLHttpClient _client;
    private readonly HttpClient _httpClient;

    public int RateLimit { get; private set; }
    public int RateRemaining { get; private set; }
    public bool HasToken { get; private set; }

    public AniClient()
    {
        _httpClient = new HttpClient();
        _client = new GraphQLHttpClient(
            new GraphQLHttpClientOptions { EndPoint = new Uri("https://graphql.anilist.co") },
            new SystemTextJsonSerializer(new JsonSerializerOptions { Converters = { new JsonStringEnumMemberConverter() } }),
            _httpClient);
    }

    private void UpdateRateLimiting(HttpHeaders response)
    {
        response.TryGetValues("X-RateLimit-Limit", out var rateLimitValues);
        response.TryGetValues("X-RateLimit-Remaining", out var rateRemainingValues);
        var rateLimit = rateLimitValues?.FirstOrDefault();
        var rateRemaining = rateRemainingValues?.FirstOrDefault();
        if (rateLimit != null)
            RateLimit = int.Parse(rateLimit);
        if (rateRemaining != null)
            RateRemaining = int.Parse(rateRemaining);
    }

    public void SetToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        HasToken = true;
    }

    public async Task<AniPagination<Media>> SearchMedia(string? query, MediaSort sort = MediaSort.Relevance, AniPaginationOptions? options = null)
    {
        options ??= new AniPaginationOptions();
        var requestQuery = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
        {
            new("pageInfo", PageInfo.Selections),
            new("media", Media.Selections)
            {
                Parameters =
                {
                    { "search", query },
                    { "sort", sort }
                }
            }
        }, new Dictionary<string, object?>
        {
            { "page", options.Index },
            { "perPage", options.Size }
        });
        var request = new GraphQLRequest(requestQuery);
        var response = await _client.SendQueryAsync<AniResponse>(request);
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        var page = (await _client.SendQueryAsync<AniResponse>(request)).Data.Page;
        return new AniPagination<Media>(page.Info, page.Media);
    }

    public async Task<Media> GetMedia(int id)
    {
        var requestQuery = GqlParser.Parse(GqlType.Query, "Media", Media.Selections, new Dictionary<string, object?> { { "id", id } });
        var request = new GraphQLRequest(requestQuery);
        var response = await _client.SendQueryAsync<AniResponse>(request);
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        return response.Data.Media;
    }

    public async Task<AniPagination<Media>> GetMediaBySeason(MediaSeason season, int? year = null, AniPaginationOptions? options = null)
    {
        year ??= DateTime.Today.Year;
        options ??= new AniPaginationOptions();
        var requestQuery = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
        {
            new("pageInfo", PageInfo.Selections),
            new("media", Media.Selections)
            {
                Parameters =
                {
                    { "season", season },
                    { "seasonYear", year }
                }
            }
        }, new Dictionary<string, object?>
        {
            { "page", options.Index },
            { "perPage", options.Size }
        });
        var request = new GraphQLRequest(requestQuery);
        var response = await _client.SendQueryAsync<AniResponse>(request);
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        return new AniPagination<Media>(response.Data.Page.Info, response.Data.Page.Media);
    }

    public async Task<User> GetUser(int? id = null)
    {
        var request = new GraphQLRequest
        {
            Query = id.HasValue
                ? GqlParser.Parse(GqlType.Query, "User", User.Selections, new Dictionary<string, object?> { { "id", id } })
                : GqlParser.Parse(GqlType.Mutation, "UpdateUser", User.Selections)
        };
        var response = await _client.SendMutationAsync<AniResponse>(request);
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        return response.Data.User ?? response.Data.UpdatedUser;
    }

    public async Task<AniPagination<MediaEntry>> GetUserEntries(int id, AniPaginationOptions? options = null)
    {
        options ??= new AniPaginationOptions();
        var requestQuery = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
        {
            new("pageInfo", PageInfo.Selections),
            new("mediaList", MediaEntry.Selections)
            {
                Parameters =
                {
                    { "userId", id }
                }
            }
        }, new Dictionary<string, object?>
        {
            { "page", options.Index },
            { "perPage", options.Size }
        });
        var request = new GraphQLRequest(requestQuery);
        var response = await _client.SendQueryAsync<AniResponse>(request);
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        return new AniPagination<MediaEntry>(response.Data.Page.Info, response.Data.Page.MediaEntries);
    }

    public async Task<AniPagination<MediaEntry>> GetUserEntries(int id, MediaType type, AniPaginationOptions? options = null)
    {
        options ??= new AniPaginationOptions();
        var requestQuery = GqlParser.Parse(GqlType.Query, "MediaListCollection", MediaEntryCollection.Selections, new Dictionary<string, object?>
        {
            { "userId", id },
            { "type", type },
            { "chunk", options.Index },
            { "perChunk", options.Size },
            { "sort", MediaEntrySort.LastUpdated }
        });
        var request = new GraphQLRequest(requestQuery);
        var response = await _client.SendQueryAsync<AniResponse>(request);
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        var data = response.Data.MediaEntryCollection.Groups.SelectMany(group => group.Entries).ToList();
        return new AniPagination<MediaEntry>(response.Data.MediaEntryCollection.HasNextChunk, data);
    }

    public async Task<MediaEntry> UpdateMediaEntry(int id, MediaEntryStatus status, int progress)
    {
        var requestQuery = GqlParser.Parse(GqlType.Mutation, "SaveMediaListEntry", MediaEntry.Selections, new Dictionary<string, object?>
        {
            { "id", id },
            { "status", status },
            { "progress", progress }
        });
        var request = new GraphQLHttpRequest(requestQuery);
        var response = (await _client.SendMutationAsync<AniResponse>(request)).Data;
        return response.UpdatedMediaEntry;
    }

}
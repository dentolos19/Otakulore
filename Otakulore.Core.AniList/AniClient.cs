using System.Net.Http.Headers;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json.Linq;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class AniClient
{

    private readonly GraphQLHttpClient _client;
    private readonly HttpClient _httpClient;

    public int RateLimit { get; private set; }
    public int RateRemaining { get; private set; }

    public bool HasToken => _httpClient.DefaultRequestHeaders.Authorization != null;

    public event EventHandler? RateUpdated;

    public AniClient()
    {
        _httpClient = new HttpClient();
        _client = new GraphQLHttpClient(new GraphQLHttpClientOptions { EndPoint = new Uri("https://graphql.anilist.co") }, new NewtonsoftJsonSerializer(), _httpClient);
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
        RateUpdated?.Invoke(this, EventArgs.Empty);
    }

    public void SetToken(string? token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = token != null ? new AuthenticationHeaderValue("Bearer", token) : null;
    }

    public async Task<KeyValuePair<string[], MediaTag[]>> GetGenresAndTags()
    {
        var request = GqlParser.Parse(GqlType.Query, new GqlSelection[]
        {
            new("GenreCollection"),
            new("MediaTagCollection", MediaTag.Selections)
        });
        var response = await _client.SendQueryAsync<JObject>(new GraphQLRequest(request));
        return new KeyValuePair<string[], MediaTag[]>(response.Data["GenreCollection"].ToObject<string[]>(), response.Data["MediaTagCollection"].ToObject<MediaTag[]>());
    }

    public async Task<AniPagination<Media>> SearchMedia(AniFilter filter, AniPaginationOptions? options = null)
    {
        options ??= new AniPaginationOptions();
        var parameters = new Dictionary<string, object?> { { "sort", filter.Sort } };
        if (filter.Query != null)
            parameters.Add("search", filter.Query);
        if (filter.Type != null)
            parameters.Add("type", filter.Type);
        if (filter.Genres is { Count: > 0 })
            parameters.Add("genre_in", filter.Genres);
        if (filter.Tags is { Count: > 0 })
            parameters.Add("tag_in", filter.Tags);
        var request = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
        {
            new("pageInfo", PageInfo.Selections),
            new("media", Media.Selections, parameters)
        }, new Dictionary<string, object?>
        {
            { "page", options.Index },
            { "perPage", options.Size }
        });
        var response = await _client.SendQueryAsync<JObject>(new GraphQLRequest(request));
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        var info = response.Data["Page"]["pageInfo"].ToObject<PageInfo>();
        var data = response.Data["Page"]["media"].ToObject<Media[]>();
        return new AniPagination<Media>(info, data);
    }

    public async Task<MediaExtra> GetMedia(int id)
    {
        var request = GqlParser.Parse(GqlType.Query, "Media", MediaExtra.Selections, new Dictionary<string, object?>
        {
            { "id", id }
        });
        var response = await _client.SendQueryAsync<JObject>(new GraphQLRequest(request));
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        return response.Data["Media"].ToObject<MediaExtra>();
    }

    public async Task<AniPagination<Media>> GetMediaBySeason(MediaSeason season, int? year = null, AniPaginationOptions? options = null)
    {
        year ??= DateTime.Today.Year;
        options ??= new AniPaginationOptions();
        var request = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
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
        var response = await _client.SendQueryAsync<JObject>(new GraphQLRequest(request));
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        var info = response.Data["Page"]["pageInfo"].ToObject<PageInfo>();
        var data = response.Data["Page"]["media"].ToObject<Media[]>();
        return new AniPagination<Media>(info, data);
    }

    public async Task<User> GetUser(int? id = null)
    {
        var request = id.HasValue
            ? GqlParser.Parse(GqlType.Query, "User", User.Selections, new Dictionary<string, object?> { { "id", id } })
            : GqlParser.Parse(GqlType.Mutation, "UpdateUser", User.Selections);
        var response = await _client.SendMutationAsync<JObject>(new GraphQLRequest(request));
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        if (response.Data.ContainsKey("User"))
            return response.Data["User"].ToObject<User>();
        return response.Data.ContainsKey("UpdateUser") ? response.Data["UpdateUser"].ToObject<User>() : new User();
    }

    public async Task<AniPagination<UserActivity>> GetUserActivities(int id, AniPaginationOptions? options = null)
    {
        options ??= new AniPaginationOptions();
        var request = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
        {
            new("pageInfo", PageInfo.Selections),
            new("activities", new GqlSelection[]
            {
                new("__typename"),
                new("... on ListActivity", UserActivity.Selections)
            }, new Dictionary<string, object?>
            {
                { "userId", id },
                { "type", "$MEDIA_LIST" },
                { "sort", "$ID_DESC" }
            })
        }, new Dictionary<string, object?>
        {
            { "page", options.Index },
            { "perPage", options.Size }
        });
        var response = await _client.SendQueryAsync<JObject>(new GraphQLRequest(request));
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        var info = response.Data["Page"]["pageInfo"].ToObject<PageInfo>();
        var data = response.Data["Page"]["activities"].ToObject<UserActivity[]>();
        data = data.Where(item => item.Id != 0).ToArray();
        return new AniPagination<UserActivity>(info, data);
    }

    public async Task<AniPagination<MediaEntry>> GetUserEntries(int id, AniPaginationOptions? options = null)
    {
        options ??= new AniPaginationOptions();
        var request = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
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
        var response = await _client.SendQueryAsync<JObject>(new GraphQLRequest(request));
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        var info = response.Data["Page"]["pageInfo"].ToObject<PageInfo>();
        var data = response.Data["Page"]["mediaList"].ToObject<MediaEntry[]>();
        return new AniPagination<MediaEntry>(info, data);
    }

    public async Task<MediaEntry> UpdateMediaEntry(int id, MediaEntryStatus status, int progress)
    {
        var request = GqlParser.Parse(GqlType.Mutation, "SaveMediaListEntry", MediaEntry.Selections, new Dictionary<string, object?>
        {
            { "mediaId", id },
            { "status", status },
            { "progress", progress }
        });
        var response = await _client.SendMutationAsync<JObject>(new GraphQLHttpRequest(request));
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        return response.Data["SaveMediaListEntry"].ToObject<MediaEntry>();
    }

    public async Task<bool> DeleteMediaEntry(int id)
    {
        var request = GqlParser.Parse(GqlType.Mutation, "DeleteMediaListEntry", new GqlSelection[] { new("deleted") }, new Dictionary<string, object?>
        {
            { "id", id }
        });
        var response = await _client.SendMutationAsync<JObject>(new GraphQLHttpRequest(request));
        UpdateRateLimiting(response.AsGraphQLHttpResponse().ResponseHeaders);
        return response.Data["DeleteMediaListEntry"].Value<bool>("deleted");
    }

}
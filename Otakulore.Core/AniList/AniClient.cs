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

    public AniClient()
    {
        _httpClient = new HttpClient();
        _client = new GraphQLHttpClient(
            new GraphQLHttpClientOptions { EndPoint = new Uri("https://graphql.anilist.co") },
            new SystemTextJsonSerializer(new JsonSerializerOptions { Converters = { new JsonStringEnumMemberConverter() } }),
            _httpClient);
    }

    public void SetToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<AniPagination<Media>> SearchMedia(string query, MediaSort sort = MediaSort.Relevance, PageOptions? options = null)
    {
        options ??= new PageOptions();
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
        }, new Dictionary<string, object>
        {
            { "page", options.Index },
            { "perPage", options.Size }
        });
        var request = new GraphQLRequest(requestQuery);
        var page = (await _client.SendQueryAsync<AniResponse>(request)).Data.Page;
        return new AniPagination<Media>(page.Info, page.Media);
    }

    public async Task<Media> GetMedia(int id)
    {
        var requestQuery = GqlParser.Parse(GqlType.Query, "Media", Media.Selections, new Dictionary<string, object> { { "id", id } });
        var request = new GraphQLRequest(requestQuery);
        var response = (await _client.SendQueryAsync<AniResponse>(request)).Data;
        return response.Media;
    }

    public async Task<AniPagination<Media>> GetMediaBySeason(MediaSeason season, int? year = null, PageOptions? options = null)
    {
        year ??= DateTime.Today.Year;
        options ??= new PageOptions();
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
        }, new Dictionary<string, object>
        {
            { "page", options.Index },
            { "perPage", options.Size }
        });
        var request = new GraphQLRequest(requestQuery);
        var page = (await _client.SendQueryAsync<AniResponse>(request)).Data.Page;
        return new AniPagination<Media>(page.Info, page.Media);
    }

    public async Task<AniPagination<MediaTrend>> GetMediaByTrend(MediaTrendSort sort = MediaTrendSort.Trending, PageOptions? options = null)
    {
        options ??= new PageOptions();
        var requestQuery = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
        {
            new("pageInfo", PageInfo.Selections),
            new("mediaTrends", new GqlSelection[]
            {
                new("media", Media.Selections)
            })
            {
                Parameters =
                {
                    { "sort", sort },
                    { "trending_not", 0 }
                }
            }
        }, new Dictionary<string, object>
        {
            { "page", options.Index },
            { "perPage", options.Size }
        });
        var request = new GraphQLRequest(requestQuery);
        var page = (await _client.SendQueryAsync<AniResponse>(request)).Data.Page;
        return new AniPagination<MediaTrend>(page.Info, page.MediaTrends);
    }

    public async Task<User> GetUser(int? id = null)
    {
        var request = new GraphQLRequest
        {
            Query = id.HasValue
                ? GqlParser.Parse(GqlType.Query, "User", User.Selections, new Dictionary<string, object> { { "id", id } })
                : GqlParser.Parse(GqlType.Mutation, "UpdateUser", User.Selections)
        };
        var response = (await _client.SendMutationAsync<AniResponse>(request)).Data;
        return response.User ?? response.UpdatedUser;
    }

    public async Task<AniPagination<MediaEntry>> GetUserEntries(int id, PageOptions? options = null)
    {
        options ??= new PageOptions();
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
        }, new Dictionary<string, object>
        {
            { "page", options.Index },
            { "perPage", options.Size }
        });
        var request = new GraphQLRequest(requestQuery);
        var response = (await _client.SendQueryAsync<AniResponse>(request)).Data;
        return new AniPagination<MediaEntry>(response.Page.Info, response.Page.MediaEntries);
    }

    public async Task<MediaEntry> UpdateMediaEntry(int id, MediaEntryStatus status, int progress)
    {
        var request = new GraphQLHttpRequest
        {
            Query = GqlParser.Parse(GqlType.Mutation, "SaveMediaListEntry", MediaEntry.Selections, new Dictionary<string, object>
            {
                { "id", id },
                { "status", status },
                { "progress", progress }
            })
        };
        var response = (await _client.SendMutationAsync<AniResponse>(request)).Data;
        return response.UpdatedMediaEntry;
    }

}
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

    public async Task<AniPagination<Media>> SearchMedia(string query, MediaSort sort = MediaSort.Relevance, int pageIndex = 1)
    {
        var request = new GraphQLRequest
        {
            Query = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
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
                { "page", pageIndex }
            })
        };
        var page = (await _client.SendQueryAsync<AniResponse>(request)).Data.Page;
        return new AniPagination<Media>(page.Info, page.Media);
    }

    public async Task<Media> GetMedia(int id)
    {
        var request = new GraphQLRequest
        {
            Query = GqlParser.Parse(GqlType.Query, "Media", Media.Selections, new Dictionary<string, object>
            {
                { "id", id }
            })
        };
        var response = await _client.SendQueryAsync<AniResponse>(request);
        return response.Data.Media;
    }

    public async Task<AniPagination<Media>> GetMediaBySeason(MediaSeason season, int? year = null, int pageIndex = 1)
    {
        year ??= DateTime.Today.Year;
        var request = new GraphQLRequest
        {
            Query = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
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
                { "page", pageIndex }
            })
        };
        var page = (await _client.SendQueryAsync<AniResponse>(request)).Data.Page;
        return new AniPagination<Media>(page.Info, page.Media);
    }

    public async Task<MediaTrend[]> GetMediaByTrends()
    {
        var request = new GraphQLRequest
        {
            Query = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
            {
                // new("pageInfo", PageInfo.Selections),
                new("mediaTrends", new GqlSelection[]
                {
                    new("media", Media.Selections)
                })
                {
                    Parameters =
                    {
                        { "sort", "$POPULARITY" }
                    }
                }
            })
        };
        var response = await _client.SendQueryAsync<AniResponse>(request);
        return response.Data.Page.MediaTrends;
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

    public async Task<AniPagination<MediaEntry>> GetUserEntries(int id, int index = 1)
    {
        var request = new GraphQLRequest
        {
            Query = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
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
                { "page", index }
            }),
            Variables = new { id }
        };
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
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

    public async Task<Media[]> SearchMedia(string query, int pageIndex = 1)
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
                        { "search", query }
                    }
                }
            }, new Dictionary<string, object>
            {
                { "page", pageIndex }
            })
        };
        var response = await _client.SendQueryAsync<AniResponse>(request);
        return response.Data.Page.Content;
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

    public async Task<Media[]> GetSeasonalMedia(MediaSeason season, int year)
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
                        { "season", season },
                        { "seasonYear", year }
                    }
                }
            })
        };
        var response = await _client.SendQueryAsync<AniResponse>(request);
        return response.Data.Page.Content;
    }

    public async Task<MediaTrend[]> GetTrendingMedia()
    {
        var request = new GraphQLRequest
        {
            Query = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
            {
                new("pageInfo", PageInfo.Selections),
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
        return response.Data.Page.TrendingContent;
    }

    public async Task<User> GetUser()
    {
        var request = new GraphQLRequest
        {
            Query = GqlParser.Parse(GqlType.Mutation, "UpdateUser", User.Selections)
        };
        var response = await _client.SendMutationAsync<AniResponse>(request);
        return response.Data.User;
    }

    public async Task<MediaList[]> GetUserList(int id, MediaType type)
    {
        var request = new GraphQLRequest
        {
            Query = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
            {
                new("mediaList", new GqlSelection[]
                {
                    new("status"),
                    new("progress"),
                    new("media", Media.Selections)
                })
                {
                    Parameters =
                    {
                        { "userId", id },
                        { "type", type }
                    }
                }
            }),
            Variables = new { id }
        };
        var response = await _client.SendQueryAsync<AniResponse>(request);
        return response.Data.Page.ContentList;
    }

}
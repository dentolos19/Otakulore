using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace Otakulore.Core.AniList;

public class AniClient
{

    private readonly GraphQLHttpClient _client;
    private readonly HttpClient _httpClient;

    public AniClient()
    {
        _httpClient = new HttpClient();
        _client = new GraphQLHttpClient(new GraphQLHttpClientOptions { EndPoint = new Uri("https://graphql.anilist.co") }, new SystemTextJsonSerializer(new JsonSerializerOptions { Converters = { new JsonStringEnumMemberConverter() } }), _httpClient);
    }

    public void SetToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<QueryResponse> SearchMedia(string search, MediaType type = MediaType.Anime, int pageIndex = 1, int count = 50)
    {
        var request = new GraphQLRequest
        {
            Query = @"
query ($search: String, $type: MediaType, $pageIndex: Int, $count: Int) {
  Page(page: $pageIndex, perPage: $count) {
    pageInfo {
      total
      currentPage
      lastPage
      hasNextPage
      perPage
    }
    media(search: $search, type: $type) {
      id
      coverImage {
        extraLarge
        large
        medium
        color
      }
      bannerImage
      title {
        romaji
        english
      }
      description(asHtml: false)
      type
      format
      status
      genres
      averageScore
      startDate {
        year
        month
        day
      }
      endDate {
        year
        month
        day
      }
      isAdult
      episodes
      duration
      chapters
    }
  }
}",
            Variables = new { search, type = type.GetEnumValue(), pageIndex, count }
        };
        var response = await _client.SendQueryAsync<QueryResponse>(request);
        return response.Data;
    }

    public async Task<QueryResponse> GetMedia(int id)
    {
        var request = new GraphQLRequest
        {
            Query = @"
query ($id: Int) {
  Media(id: $id) {
    id
    coverImage {
      extraLarge
      large
      medium
      color
    }
    bannerImage
    title {
      romaji
      english
    }
    description(asHtml: false)
    type
    format
    status
    genres
    averageScore
    startDate {
      year
      month
      day
    }
    endDate {
      year
      month
      day
    }
    isAdult
    episodes
    duration
    chapters
  }
}",
            Variables = new { id }
        };
        var response = await _client.SendQueryAsync<QueryResponse>(request);
        return response.Data;
    }

    public async Task<QueryResponse> GetSeasonalMedia(MediaSeason season, int year)
    {
        var request = new GraphQLRequest
        {
            Query = @"
query ($season: MediaSeason, $year: Int) {
  Page {
    media(season: $season, seasonYear: $year) {
      id
      coverImage {
        extraLarge
        large
        medium
        color
      }
      bannerImage
      title {
        romaji
        english
      }
      description(asHtml: false)
      type
      format
      status
      genres
      averageScore
      startDate {
        year
        month
        day
      }
      endDate {
        year
        month
        day
      }
      isAdult
      episodes
      duration
      chapters
    }
  }
}",
            Variables = new { season = season.GetEnumValue(), year }
        };
        var response = await _client.SendQueryAsync<QueryResponse>(request);
        return response.Data;
    }

    public async Task<QueryResponse> GetTrendingMedia()
    {
        var request = new GraphQLRequest
        {
            Query = @"
{
  Page {
    pageInfo {
      total
      currentPage
      lastPage
      hasNextPage
      perPage
    }
    mediaTrends {
      media {
        id
        coverImage {
          extraLarge
          large
          medium
          color
        }
        bannerImage
        title {
          romaji
          english
        }
        description(asHtml: false)
        type
        format
        status
        genres
        averageScore
        startDate {
          year
          month
          day
        }
        endDate {
          year
          month
          day
        }
        isAdult
        episodes
        duration
        chapters
      }
    }
  }
}"
        };
        var response = await _client.SendQueryAsync<QueryResponse>(request);
        return response.Data;
    }

    public async Task<MutationResponse> GetUser()
    {
        var request = new GraphQLRequest
        {
            Query = @"
mutation {
  UpdateUser {
    id
    name
    avatar {
      large
      medium
    }
    bannerImage
    about(asHtml: false)
  }
}"
        };
        var response = await _client.SendMutationAsync<MutationResponse>(request);
        return response.Data;
    }

}
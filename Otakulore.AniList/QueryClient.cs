using System.Text.Json;
using System.Text.Json.Serialization;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace Otakulore.AniList;

public class QueryClient
{

    private readonly GraphQLHttpClient _client;

    public QueryClient()
    {
        _client = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer(new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumMemberConverter() }
        }));
    }

    public async Task<QueryResponse> Search(string search, MediaType type = MediaType.Anime, int pageIndex = 1, int count = 50)
    {
        var request = new GraphQLRequest
        {
            Query = @"
query ($search: String, $type: MediaType, $page: Int, $perPage: Int) {
  Page(page: $page, perPage: $perPage) {
    pageInfo {
      total
      currentPage
      lastPage
      hasNextPage
      perPage
    }
    media(search: $search, type: $type) {
      id
      idMal
      coverImage {
        extraLarge
        large
        medium
        color
      }
      title {
        romaji
        english
      }
      description(asHtml: false)
      type
      format
      status
      episodes
      duration
      chapters
    }
  }
}
",
            Variables = new { search, type = type.ToEnumString(), page = pageIndex, perPage = count }
        };
        var response = await _client.SendQueryAsync<QueryResponse>(request);
        return response.Data;
    }

    public async Task<QueryResponse> GetMedia(int id)
    {
        var request = new GraphQLRequest
        {
            Query = @"
query($id: Int) {
  Page(page: $page, perPage: $perPage) {
    pageInfo {
      total
      currentPage
      lastPage
      hasNextPage
      perPage
    }
    media(search: $search, type: $type) {
      id
      idMal
      title {
        romaji
        english
      }
      type
      format
      status
      genres
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
      episodes
      duration
      chapters
    }
  }
}
",
            Variables = new { id }
        };
        var response = await _client.SendQueryAsync<QueryResponse>(request);
        return response.Data;
    }

}
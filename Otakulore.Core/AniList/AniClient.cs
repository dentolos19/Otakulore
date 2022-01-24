using System.Text.Json;
using System.Text.Json.Serialization;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace Otakulore.Core.AniList;

public class AniClient
{

    public AniClient()
    {
        var client = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer(new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumMemberConverter() }
        }));
        Query = new QueryClient(client);
        Mutation = new QueryClient(client);
    }

    public QueryClient Query { get; }
    public QueryClient Mutation { get; }

}
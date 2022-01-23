using GraphQL.Client.Http;

namespace Otakulore.AniList;

public class MutationClient
{

    private readonly GraphQLHttpClient _client;

    public MutationClient(GraphQLHttpClient client)
    {
        _client = client;
    }

}
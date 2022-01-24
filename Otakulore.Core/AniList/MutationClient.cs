using GraphQL.Client.Http;

namespace Otakulore.Core.AniList;

public class MutationClient
{

    private readonly GraphQLHttpClient _client;

    public MutationClient(GraphQLHttpClient client)
    {
        _client = client;
    }

}
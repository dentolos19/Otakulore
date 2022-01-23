using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Otakulore.AniList.Tests;

public class QueryTests
{

    private QueryClient _client = new();

    [TestCase("demon slayer")]
    public async Task SearchTest(string query)
    {
        var response = await _client.Search(query);
        Assert.IsNotNull(response);
        var dump = ObjectDumper.Dump(response);
        Console.WriteLine(dump);
    }

}
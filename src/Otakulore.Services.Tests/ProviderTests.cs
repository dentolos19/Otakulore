using NUnit.Framework;
using Otakulore.Services.Providers;

namespace Otakulore.Services.Tests;

public class ProviderTests
{

    [Test]
    public async Task GogoanimeSearchTest()
    {
        var provider = new GogoanimeProvider();
        var results = await provider.SearchAsync("one piece");
        if (results == null)
            Assert.Fail();
        foreach (var item in results)
            Console.WriteLine(item.Title);
        Assert.Pass();
    }

    [Test]
    public async Task ManganatoSearchTest()
    {
        var provider = new ManganatoProvider();
        var results = await provider.SearchAsync("one piece");
        if (results == null)
            Assert.Fail();
        foreach (var item in results)
            Console.WriteLine(item.Title);
        Assert.Pass();
    }

}
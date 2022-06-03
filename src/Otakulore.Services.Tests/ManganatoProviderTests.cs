using NUnit.Framework;
using Otakulore.Services.Providers;

namespace Otakulore.Services.Tests;

public class ManganatoProviderTests
{

    private IProvider _provider;

    [OneTimeSetUp]
    public void SetupTests()
    {
        _provider = new ManganatoProvider();
    }

    [Test]
    public async Task SearchSourcesTest()
    {
        var results = await _provider.SearchSourcesAsync("one piece");
        if (results == null)
            Assert.Fail();
        foreach (var item in results)
            Console.WriteLine(item.Title);
        Assert.Pass();
    }

    [Test]
    public async Task GetContentTest()
    {
        var source = (await _provider.SearchSourcesAsync("one piece")).First();
        var results = await _provider.GetContentAsync(source);
        if (results == null)
            Assert.Fail();
        foreach (var item in results)
            Console.WriteLine(item.Name);
        Assert.Pass();
    }

}
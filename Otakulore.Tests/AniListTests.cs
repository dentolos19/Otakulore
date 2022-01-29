using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Otakulore.Core.AniList;

namespace Otakulore.Tests;

public class AniListTests
{

    private readonly AniClient _client = new();

    [TestCase("demon slayer", MediaType.Anime)]
    [TestCase("black clover", MediaType.Manga)]
    public async Task SearchMediaTest(string query, MediaType type)
    {
        var response = await _client.SearchMedia(query, type);
        Assert.IsNotNull(response);
        var dump = ObjectDumper.Dump(response);
        Console.WriteLine(dump);
    }

    [TestCase(1)]
    public async Task GetMediaTest(int id)
    {
        var response = await _client.GetMedia(id);
        Assert.IsNotNull(response);
        var dump = ObjectDumper.Dump(response);
        Console.WriteLine(dump);
    }

    [TestCase(MediaSeason.Winter)]
    [TestCase(MediaSeason.Spring)]
    [TestCase(MediaSeason.Summer)]
    [TestCase(MediaSeason.Fall)]
    public async Task GetSeasonalMediaTest(MediaSeason season)
    {
        var year = DateTime.Today.Year;
        var response = await _client.GetSeasonalMedia(season, year);
        Assert.IsNotNull(response);
        var dump = ObjectDumper.Dump(response);
        Console.WriteLine(dump);
    }

    [Test]
    public async Task GetTrendingMediaTest()
    {
        var response = await _client.GetTrendingMedia();
        Assert.IsNotNull(response);
        var dump = ObjectDumper.Dump(response);
        Console.WriteLine(dump);
    }

    [TestCase(100)]
    public async Task GetUserListTest(int id)
    {
        var response = await _client.GetUserList(id, MediaType.Anime);
        Assert.IsNotNull(response);
        var dump = ObjectDumper.Dump(response);
        Console.WriteLine(dump);
    }

}
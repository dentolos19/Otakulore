using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Otakulore.Core.AniList;

namespace Otakulore.Tests;

[TestFixture]
internal static class AniListTests
{

    private static readonly AniClient Client = new();

    [TearDown]
    public static void AfterTest()
    {
        Console.WriteLine($"Rate Remaining: {Client.RateRemaining}/{Client.RateLimit}");
    }

    [Test]
    public static async Task GetGenresAndTagsTest()
    {
        var response = await Client.GetGenresAndTags();
        Console.WriteLine(ObjectDumper.Dump(response));
    }

    [Test]
    public static async Task SearchMediaTest()
    {
        var response = await Client.SearchMedia(new AniFilter { Query = "test", Type = MediaType.Anime, Genres = { "Action" } });
        Console.WriteLine(ObjectDumper.Dump(response));
    }

    [Test]
    public static async Task GetMediaTest()
    {
        var response = await Client.GetMedia(1);
        Console.WriteLine(ObjectDumper.Dump(response));
    }

    [TestCase(MediaSeason.Winter)]
    [TestCase(MediaSeason.Spring)]
    [TestCase(MediaSeason.Summer)]
    [TestCase(MediaSeason.Fall)]
    public static async Task GetMediaBySeasonTest(MediaSeason season)
    {
        var response = await Client.GetMediaBySeason(season);
        Console.WriteLine(ObjectDumper.Dump(response));
    }

    [Test]
    public static async Task GetUserTest()
    {
        var response = await Client.GetUser(1);
        Console.WriteLine(ObjectDumper.Dump(response));
    }

    [Test]
    public static async Task GetUserActivitiesTest()
    {
        var response = await Client.GetUserActivities(1);
        Console.WriteLine(ObjectDumper.Dump(response));
    }

    [Test]
    public static async Task GetUserEntriesTest()
    {
        var response = await Client.GetUserEntries(100);
        Console.WriteLine(ObjectDumper.Dump(response));
    }

}
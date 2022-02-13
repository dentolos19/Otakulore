using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Otakulore.Core.AniList;

namespace Otakulore.Tests;

internal static class ClientTests
{

    private static readonly AniClient Client = new();

    [Test]
    public static async Task SearchMediaTest()
    {
        var response = await Client.SearchMedia("test");
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
    public static async Task GetUserEntriesTest()
    {
        var response = await Client.GetUserEntries(100);
        Console.WriteLine(ObjectDumper.Dump(response));
    }

}
using System;
using NUnit.Framework;
using Otakulore.Core.Providers;

namespace Otakulore.Tests;

internal static class ProvidersTest
{

    [Test]
    public static void AnimeKisaTest()
    {
        var provider = new AnimeKisaProvider();
        var sources = provider.GetSources("test");
        Console.WriteLine(ObjectDumper.Dump(sources));
        var content = provider.GetContents(sources[0]);
        Console.WriteLine(ObjectDumper.Dump(content));
    }

    [Test]
    public static void MangakakalotTest()
    {
        var provider = new MangakakalotProvider();
        var sources = provider.GetSources("test");
        Console.WriteLine(ObjectDumper.Dump(sources));
        var content = provider.GetContents(sources[0]);
        Console.WriteLine(ObjectDumper.Dump(content));
    }

    [Test]
    public static void NovelhallTest()
    {
        var provider = new NovelhallProvider();
        var sources = provider.GetSources("test");
        Console.WriteLine(ObjectDumper.Dump(sources));
        var content = provider.GetContents(sources[0]);
        Console.WriteLine(ObjectDumper.Dump(content));
    }

}
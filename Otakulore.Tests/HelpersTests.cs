using System;
using NUnit.Framework;
using Otakulore.Core.AniList;
using Otakulore.Core.Helpers;

namespace Otakulore.Tests;

public class HelpersTests
{

    [Test]
    public void GqlParsingTest()
    {
        var query = GqlParser.Parse(GqlType.Query, "Page", new GqlSelection[]
        {
            new("pageInfo", PageInfo.Selections),
            new("mediaTrends", new GqlSelection[]
            {
                new("media", Media.Selections)
            }) { Parameters = { { "sort", "$POPULARITY" } } }
        });
        Console.WriteLine(query);
    }

}
using System;
using JikanDotNet;

namespace Otakulore.Core;

public static class Utilities
{

    public static Seasons GetAnimeSeason(DateOnly date)
    {
        var doy = date.DayOfYear - Convert.ToInt32(DateTime.IsLeapYear(date.Year) && date.DayOfYear > 59);
        return doy switch
        {
            < 80 or >= 355 => Seasons.Winter,
            >= 80 and < 172 => Seasons.Spring,
            >= 172 and < 266 => Seasons.Summer,
            _ => Seasons.Fall
        };
    }

}
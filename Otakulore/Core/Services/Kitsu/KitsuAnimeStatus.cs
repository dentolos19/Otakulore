using System.ComponentModel;

namespace Otakulore.Core.Services.Kitsu
{

    public enum KitsuAnimeStatus
    {

        Unknown,

        Releasing,
        Completed,
        [Description("TBA")] ToBeAired,
        Unreleased,
        Upcoming

    }

}
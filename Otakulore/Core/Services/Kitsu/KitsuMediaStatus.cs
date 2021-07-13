using System.ComponentModel;

namespace Otakulore.Core.Services.Kitsu
{

    public enum KitsuMediaStatus
    {

        Unknown,

        Releasing,
        [Description("Finished")] Completed,
        [Description("TBA")] Tba,
        Unreleased,
        Upcoming

    }

}
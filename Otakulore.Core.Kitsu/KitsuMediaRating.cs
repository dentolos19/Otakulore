using System.Runtime.Serialization;

namespace Otakulore.Core.Kitsu
{

    public enum KitsuMediaRating
    {

        [EnumMember(Value = "G")] GeneralAudiences,
        [EnumMember(Value = "PG")] ParentalGuidanceSuggested,
        [EnumMember(Value = "R")] Restricted,
        [EnumMember(Value = "R18")] Explicit,

    }

}
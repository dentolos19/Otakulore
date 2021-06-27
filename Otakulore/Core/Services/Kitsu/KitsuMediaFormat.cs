using System.ComponentModel;
using System.Runtime.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public enum KitsuMediaFormat
    {

        [EnumMember(Value = "ONA")] [Description("ONA")] Ona,
        [EnumMember(Value = "OVA")] [Description("OVA")] Ova,
        [EnumMember(Value = "TV")] [Description("TV")] Tv,
        [EnumMember(Value = "movie")] Movie,
        [EnumMember(Value = "music")] Music,
        [EnumMember(Value = "special")] Special

    }

}
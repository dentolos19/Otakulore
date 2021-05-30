using System.Runtime.Serialization;

namespace Otakulore.Core.Kitsu
{

    public enum KitsuMangaFormat
    {

        [EnumMember(Value = "doujin")] Doujin,
        [EnumMember(Value = "manga")] Manga,
        [EnumMember(Value = "manhua")] Manhua,
        [EnumMember(Value = "manhwa")] Manhwa,
        [EnumMember(Value = "novel")] Novel,
        [EnumMember(Value = "oel")] Oel,
        [EnumMember(Value = "oneshot")] OneShot,

    }

}
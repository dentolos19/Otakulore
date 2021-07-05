using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuAnimeStatusConverter : JsonConverter<KitsuAnimeStatus>
    {

        public override KitsuAnimeStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.GetString())
            {
                case "current":
                    return KitsuAnimeStatus.Releasing;
                case "finished":
                    return KitsuAnimeStatus.Completed;
                case "tba":
                    return KitsuAnimeStatus.ToBeAired;
                case "unreleased":
                    return KitsuAnimeStatus.Unreleased;
                case "upcoming":
                    return KitsuAnimeStatus.Upcoming;
                default:
                    return KitsuAnimeStatus.Unknown;
            }
        }

        public override void Write(Utf8JsonWriter writer, KitsuAnimeStatus value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

    }

}
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuMediaStatusConverter : JsonConverter<KitsuMediaStatus>
    {

        public override KitsuMediaStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.GetString())
            {
                case "current":
                    return KitsuMediaStatus.Releasing;
                case "finished":
                    return KitsuMediaStatus.Completed;
                case "tba":
                    return KitsuMediaStatus.Tba;
                case "unreleased":
                    return KitsuMediaStatus.Unreleased;
                case "upcoming":
                    return KitsuMediaStatus.Upcoming;
                default:
                    return KitsuMediaStatus.Unknown;
            }
        }

        public override void Write(Utf8JsonWriter writer, KitsuMediaStatus value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

    }

}
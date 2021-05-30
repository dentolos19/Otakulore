using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Otakulore.Core
{

    public class Configuration
    {

        private static readonly string SourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Otakulore.cfg");
        private static readonly XmlSerializer ConfigSerializer = new(typeof(Configuration));

        public List<string> FavoritesList { get; set; } = new();

        public void SaveConfig()
        {
            var stream = new FileStream(SourcePath, FileMode.Create);
            ConfigSerializer.Serialize(stream, this);
            stream.Close();
        }

        public static Configuration LoadConfig()
        {
            if (!File.Exists(SourcePath))
                return new Configuration();
            var stream = new FileStream(SourcePath, FileMode.Open);
            var result = (Configuration)ConfigSerializer.Deserialize(stream)!;
            stream.Close();
            return result;
        }

    }

}
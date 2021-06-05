using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Otakulore.Core
{

    public class UserData
    {

        private static readonly string SourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Otakulore.usrdat");
        private static readonly XmlSerializer DataSerializer = new(typeof(UserData));

        public bool EnableDarkMode { get; set; }
        public bool EnableDiscordRichPresence { get; set; } = true;
        public List<string>? FavoritesList { get; set; } = new();

        public void SaveData()
        {
            var stream = new FileStream(SourcePath, FileMode.Create);
            DataSerializer.Serialize(stream, this);
            stream.Close();
        }

        public static UserData LoadData()
        {
            if (!File.Exists(SourcePath))
                return new UserData();
            var stream = new FileStream(SourcePath, FileMode.Open);
            var result = (UserData)DataSerializer.Deserialize(stream)!;
            stream.Close();
            return result;
        }

    }

}
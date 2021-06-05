using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Otakulore.Core
{

    public class UserData
    {

        private static readonly string UserDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Otakulore." + Environment.UserName + ".userdata");
        private static readonly JsonSerializerOptions SerializationOptions = new() { WriteIndented = true };

        public bool EnableDarkMode { get; set; }
        public bool EnableDiscordRichPresence { get; set; } = true;
        public List<string>? FavoritesList { get; set; } = new();

        public void SaveData()
        {
            var jsonData = JsonSerializer.Serialize(this, SerializationOptions);
            File.WriteAllText(UserDataPath, jsonData);
        }

        public static UserData LoadData()
        {
            if (!File.Exists(UserDataPath))
                return new UserData();
            var jsonData = File.ReadAllText(UserDataPath);
            return JsonSerializer.Deserialize<UserData>(jsonData)!;
        }

    }

}
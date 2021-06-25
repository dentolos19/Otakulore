using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Windows.Storage;

namespace Otakulore.Core
{

    public class UserData
    {

        private static readonly string DataFilePath = Path.Combine(ApplicationData.Current.RoamingFolder.Path, "userdata.json");

        public List<string> FavoriteList { get; set; } = new List<string>();

        public void SaveData()
        {
            File.WriteAllText(DataFilePath, JsonSerializer.Serialize(this));
        }

        public static UserData LoadData()
        {
            if (!File.Exists(DataFilePath))
                return new UserData();
            return JsonSerializer.Deserialize<UserData>(File.ReadAllText(DataFilePath));
        }

    }

}
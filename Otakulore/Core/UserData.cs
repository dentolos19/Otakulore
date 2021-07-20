using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Windows.Storage;
using Otakulore.Core.Services.Anime.Providers;
using Otakulore.Core.Services.Manga.Providers;
using Otakulore.Core.Services.Common;

namespace Otakulore.Core
{

    public class UserData
    {

        private static readonly string DataFilePath = Path.Combine(ApplicationData.Current.RoamingFolder.Path, "userdata.json");
        
        public string DefaultAnimeProvider { get; set; } = new AnimeKisaProvider().Id;
        public string DefaultMangaProvider { get; set; } = new ManganatoProvider().Id;
        public List<CommonMediaDetails> FavoriteList { get; set; } = new List<CommonMediaDetails>();

        public void SaveData()
        {
            File.WriteAllText(DataFilePath, JsonSerializer.Serialize(this));
        }

        public static UserData LoadData()
        {
            try
            {
                return !File.Exists(DataFilePath) ? new UserData() : JsonSerializer.Deserialize<UserData>(File.ReadAllText(DataFilePath));
            }
            catch
            {
                return new UserData();
            }
        }

    }

}
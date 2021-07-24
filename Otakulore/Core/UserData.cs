using Otakulore.Core.Services.Anime.Providers;
using Otakulore.Core.Services.Common;
using Otakulore.Core.Services.Manga.Providers;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Windows.Storage;

namespace Otakulore.Core
{

    public class UserData
    {

        private static readonly string DataFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userdata.json");

        public string DefaultAnimeProvider { get; set; } = new AnimeKisaProvider().Id;
        public string DefaultMangaProvider { get; set; } = new ManganeloProvider().Id;
        public List<CommonMediaDetails> FavoriteList { get; set; } = new List<CommonMediaDetails>();

        public void SaveData()
        {
            var data = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DataFilePath, data);
        }

        public static UserData LoadData()
        {
            try
            {
                if (!File.Exists(DataFilePath))
                    return new UserData();
                var data = File.ReadAllText(DataFilePath);
                return JsonSerializer.Deserialize<UserData>(data);
            }
            catch
            {
                return new UserData();
            }
        }

    }

}
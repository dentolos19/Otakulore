using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Windows.Storage;
using Otakulore.Models;

namespace Otakulore.Core;

public class Settings
{

    private static string FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "settings.json");

    public bool LoadSeleniumAtStartup { get; set; }
    public IList<MediaItemModel> Favorites { get; set; } = new List<MediaItemModel>();

    public void Save()
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public static Settings Load()
    {
        if (!File.Exists(FilePath))
            return new Settings();
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<Settings>(json);
    }

}
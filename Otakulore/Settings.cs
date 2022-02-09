using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Windows.Storage;
using Otakulore.Core.AniList;

namespace Otakulore;

public class Settings
{

    private static readonly string FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "settings.json");

    public string? UserToken { get; set; }
    public IList<Media> Favorites { get; set; } = new List<Media>();

    public void Save()
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public static Settings Load()
    {
        try
        {
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<Settings>(json);
        }
        catch
        {
            return new Settings();
        }
    }

}
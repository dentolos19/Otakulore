using System.IO;
using System.Text.Json;
using Windows.Storage;

namespace Otakulore.Core;

public class Settings
{

    private static readonly string FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "settings.json");

    public string? UserToken { get; set; }

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
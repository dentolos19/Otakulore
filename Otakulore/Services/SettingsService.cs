using System.Text.Json;

namespace Otakulore.Services;

public class SettingsService
{

    private static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "settings.json");

    public int ThemeIndex { get; set; }

    public void Save()
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public static SettingsService Initialize()
    {
        if (!File.Exists(FilePath))
            return new SettingsService();
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<SettingsService>(json);
    }

}
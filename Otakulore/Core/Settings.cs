using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Otakulore.Models;

namespace Otakulore.Core;

public class Settings
{

    private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.settings.json");

    public bool UseEnglishTitles { get; set; } = true;
    public IList<MediaItemModel> Favorites { get; set; } = new List<MediaItemModel>();

    public void Save()
    {
        var fileContent = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, fileContent);
    }

    public static Settings Load()
    {
        if (!File.Exists(FilePath))
            return new Settings();
        var fileContent = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<Settings>(fileContent);
    }

}
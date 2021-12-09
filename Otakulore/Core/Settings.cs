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

    public IList<MediaItemModel> Favorites { get; set; } = new List<MediaItemModel>();

    public void Save()
    {
        File.WriteAllText(FilePath, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
    }

    public static Settings Load()
    {
        return !File.Exists(FilePath)
            ? new Settings()
            : JsonSerializer.Deserialize<Settings>(File.ReadAllText(FilePath));
    }

}
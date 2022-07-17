using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Otakulore.Services;

public class SettingsService
{

    public int ThemeIndex
    {
        get => GetValue(0);
        set => SetValue(value);
    }

    public static SettingsService Initialize()
    {
        return new SettingsService();
    }

    private static TObject? GetValue<TObject>(TObject? defaultValue = default, [CallerMemberName] string propertyName = null!)
    {
        if (IsStoreableType(typeof(TObject)))
        {
            return Preferences.Default.Get(propertyName, defaultValue);
        }
        var json = Preferences.Default.Get(propertyName, string.Empty);
        return string.IsNullOrEmpty(json)
            ? defaultValue
            : JsonSerializer.Deserialize<TObject>(json);
    }

    private static void SetValue(object value, [CallerMemberName] string propertyName = null!)
    {
        if (IsStoreableType(value.GetType()))
        {
            Preferences.Default.Set(propertyName, value);
        }
        else
        {
            var json = JsonSerializer.Serialize(value);
            Preferences.Default.Set(propertyName, json);
        }
    }

    private static bool IsStoreableType(Type type)
    {
        return type == typeof(bool) ||
               type == typeof(double) ||
               type == typeof(int) ||
               type == typeof(float) ||
               type == typeof(long) ||
               type == typeof(string) ||
               type == typeof(DateTime);
    }

}
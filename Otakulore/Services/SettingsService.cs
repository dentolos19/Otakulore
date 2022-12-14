using System.Runtime.CompilerServices;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Helpers;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Services;

[SingletonService]
public class SettingsService : ObservableObject
{

    public static SettingsService Instance => MauiHelper.GetService<SettingsService>()!;

    public Theme AppTheme
    {
        get => GetValue(Theme.Dark);
        set => SetValue(value);
    }

    public string? AccessToken
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    private static TObject? GetValue<TObject>(TObject? defaultValue = default, [CallerMemberName] string propertyName = null!)
    {
        var valueType = typeof(TObject);
        if (
            valueType == typeof(bool) ||
            valueType == typeof(double) ||
            valueType == typeof(int) ||
            valueType == typeof(float) ||
            valueType == typeof(long) ||
            valueType == typeof(string) ||
            valueType == typeof(DateTime)
        )
            return Preferences.Default.Get(propertyName, defaultValue);
        var json = Preferences.Default.Get(propertyName, string.Empty);
        return string.IsNullOrEmpty(json)
            ? defaultValue
            : JsonSerializer.Deserialize<TObject>(json);
    }

    private static void SetValue<TObject>(TObject? value, [CallerMemberName] string propertyName = null!)
    {
        if (value is null)
        {
            if (Preferences.Default.ContainsKey(propertyName))
                Preferences.Default.Remove(propertyName);
            return;
        }
        var valueType = value.GetType();
        if (
            valueType == typeof(bool) ||
            valueType == typeof(double) ||
            valueType == typeof(int) ||
            valueType == typeof(float) ||
            valueType == typeof(long) ||
            valueType == typeof(string) ||
            valueType == typeof(DateTime)
        )
        {
            Preferences.Default.Set(propertyName, value);
        }
        else
        {
            var json = JsonSerializer.Serialize(value);
            Preferences.Default.Set(propertyName, json);
        }
    }

}
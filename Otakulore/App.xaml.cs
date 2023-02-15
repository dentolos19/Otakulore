using Otakulore.Pages;
using Otakulore.Resources;
using Otakulore.Resources.Themes;
using Otakulore.Services;
using Otakulore.Utilities.Enumerations;

namespace Otakulore;

public partial class App
{
    public App()
    {
        InitializeComponent();
        UpdateTheme(SettingsService.Instance.AppTheme);
        MainPage = new MainPage();
    }

    public static void UpdateTheme(Theme theme)
    {
        var dictionaries = Current!.Resources.MergedDictionaries;
        dictionaries.Clear();
        dictionaries.Add(new Default());
        dictionaries.Add(theme switch
        {
            Theme.Green => new Green(),
            Theme.Lavender => new Lavender(),
            _ => new Dark()
        });
    }
}
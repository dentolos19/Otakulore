namespace Otakulore.Models;

public class SettingsViewModel : BaseViewModel
{

    public bool LoadSeleniumAtStartup
    {
        get => App.Settings.LoadSeleniumAtStartup;
        set
        {
            App.Settings.LoadSeleniumAtStartup = value;
            UpdateProperty();
        }
    }

}
namespace Otakulore.Models;

public class SettingsViewModel : BaseViewModel
{

    public bool UseEnglishTitles
    {
        get => App.Settings.UseEnglishTitles;
        set
        {
            App.Settings.UseEnglishTitles = value;
            UpdateProperty();
        }
    }

    public bool FilterAdultTitles
    {
        get => App.Settings.FilterAdultTitles;
        set
        {
            App.Settings.FilterAdultTitles = value;
            UpdateProperty();
        }
    }

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
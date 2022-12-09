using Otakulore.Pages;
using Otakulore.Services;

namespace Otakulore;

public partial class App
{

    public App()
    {
        InitializeComponent();
        if (SettingsService.Instance.AccessToken is not null)
            DataService.Instance.Client.TryAuthenticateAsync(SettingsService.Instance.AccessToken);
        MainPage = new MainPage();
    }

}
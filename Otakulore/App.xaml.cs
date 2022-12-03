using Otakulore.Pages;

namespace Otakulore;

public partial class App
{

    public App()
    {
        InitializeComponent();
        Current.UserAppTheme = AppTheme.Dark;
        MainPage = new MainPage();
    }

}
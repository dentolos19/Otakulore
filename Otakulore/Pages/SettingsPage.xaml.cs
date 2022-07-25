using Otakulore.Models;

namespace Otakulore.Pages;

public partial class SettingsPage
{

    private SettingsViewModel Context => (SettingsViewModel)BindingContext;

    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SettingsViewModel>();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await Context.CheckAuthenticationStatus();
    }

}
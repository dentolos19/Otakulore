using Otakulore.Models;

namespace Otakulore.Pages;

public partial class SettingsPage
{

    private SettingsPageModel Context => (SettingsPageModel)BindingContext;

    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SettingsPageModel>();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await Context.CheckAuthenticationStatus();
    }

}
using Otakulore.Models;

namespace Otakulore.Pages;

public partial class SettingsPage
{

    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SettingsViewModel>();
    }

}
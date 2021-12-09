using Otakulore.Models;

namespace Otakulore.Views;

public partial class SettingsPage
{

    public SettingsPage()
    {
        InitializeComponent();
        foreach (var provider in App.Providers)
            ProviderList.Items.Add(new ProviderItemModel(provider));
    }

}
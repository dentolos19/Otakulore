using Otakulore.Models;
using Otakulore.Services;

namespace Otakulore.Pages;

public partial class MainPage
{
    public new INavigation Navigation => ((NavigationPage)Detail).Navigation;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<MainPageModel>();
        Loaded += async (_, _) =>
        {
            if (SettingsService.Instance.AccessToken is not null)
                await DataService.Instance.Client.TryAuthenticateAsync(SettingsService.Instance.AccessToken);
            FlyoutCollection.SelectedItem = FlyoutCollection.ItemsSource.Cast<FlyoutItemModel>().First();
        };
    }

    private void OnSelectionChanged(object? sender, SelectedItemChangedEventArgs args)
    {
        if (args.SelectedItem is not FlyoutItemModel item)
            return;
        var navigationPage = new NavigationPage(MauiHelper.ActivatePage(item.Type));
        Detail = navigationPage;
        IsPresented = false;
    }
}
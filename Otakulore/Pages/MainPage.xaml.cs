using Otakulore.Models;

namespace Otakulore.Pages;

public partial class MainPage
{

    public new INavigation Navigation => ((NavigationPage)Detail).Navigation;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<MainPageModel>();
        Loaded += (_, _) => FlyoutCollection.SelectedItem = FlyoutCollection.ItemsSource.Cast<FlyoutItemModel>().First();
    }

    private void OnSelectionChanged(object? sender, SelectedItemChangedEventArgs args)
    {
        if (args.SelectedItem is not FlyoutItemModel item)
            return;
        Detail = new NavigationPage(MauiHelper.ActivatePage(item.Type));
        IsPresented = false;
    }

}
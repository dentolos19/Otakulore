using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class HomePage
{

    public HomePage()
    {
        InitializeComponent();
    }

    private HomeViewModel ViewModel => (HomeViewModel)DataContext;

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        ViewModel.Load();
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), item.Data);
    }

}
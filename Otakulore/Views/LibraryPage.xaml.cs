using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class LibraryPage
{

    public LibraryPage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (App.Settings.UserToken == null)
        {
            Frame.Navigate(typeof(LoginPage));
            return;
        }
        App.Client.SetToken(App.Settings.UserToken);
        var user = await App.Client.GetUser();
        DataContext = new LibraryViewModel
        {
            ImageUrl = user.User.Avatar.LargeImageUrl,
            Name = user.User.Name
        };
        var list = await App.Client.GetUserList(user.User.Id);
        foreach (var entry in list.Page.ContentList)
            List.Items.Add(new MediaItemModel(entry.Media));
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), item.Data);
    }

}
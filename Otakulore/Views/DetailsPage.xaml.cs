using System;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;
using Otakulore.Views.Dialogs;

namespace Otakulore.Views;

public sealed partial class DetailsPage
{

    private Media _media;

    private DetailsViewModel ViewModel => (DetailsViewModel)DataContext;

    public DetailsPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not Media data)
            return;
        _media = data;
        DataContext = DetailsViewModel.Create(_media);
        foreach (var provider in App.Providers)
            switch (_media.Type)
            {
                case MediaType.Anime when provider is IAnimeProvider:
                case MediaType.Manga when provider is IMangaProvider:
                    ProviderList.Items.Add(new ProviderItemModel(provider));
                    break;
            }
    }

    private async void OnProviderClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is not ProviderItemModel item)
            return;
        var dialog = new SearchProviderDialog(item.Provider, ViewModel.Title);
        await dialog.ShowAsync();
    }

    private async void OnTrackRequested(object sender, RoutedEventArgs args)
    {
        if (App.Settings.UserToken == null)
        {
            var model = new NotificationDataModel
            {
                Message = "This feature requires an authenticated AniList account.",
                ContinueText = "Login"
            };
            model.ContinueClicked += (_, _) => App.NavigateContent(typeof(ProfileLoginPage));
            App.ShowNotification(model);
            return;
        }
        var dialog = new ManageTrackerDialog();
        await dialog.ShowAsync();
    }

    private void OnUpdateFavorite(object sender, RoutedEventArgs args)
    {
        var item = App.Settings.Favorites.FirstOrDefault(item => item.Id == _media.Id);
        if (ViewModel.IsFavorite)
        {
            if (item == null)
                App.Settings.Favorites.Add(_media);
        }
        else
        {
            if (item != null)
                App.Settings.Favorites.Remove(item);
        }
    }

}
using System;
using System.Linq;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;
using Otakulore.Views.Pages;

namespace Otakulore.Views.Panels;

public sealed partial class ProfileOverviewPanel
{

    public ProfileOverviewPanel()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not User user)
            return;
        PersonPictureImage.ProfilePicture = new BitmapImage(new Uri(user.Avatar.LargeImageUrl));
        PersonNameText.Text = user.Name;
        AboutText.Text = user.About != null ? Utilities.ConvertHtmlToMarkdown(user.About) : "This user does not have an about description!";
        var source = new IncrementalSource<MediaItemModel>(async (index, size) => (await App.Client.GetUserActivities(user.Id, new AniPaginationOptions(index + 1, size))).Data.Select(activity => new MediaItemModel(activity)));
        var collection = new IncrementalLoadingCollection<IncrementalSource<MediaItemModel>, MediaItemModel>(source, 50);
        collection.OnStartLoading += () => ActivityListIndicator.IsActive = true;
        collection.OnEndLoading += () => ActivityListIndicator.IsActive = false;
        ActivityList.ItemsSource = collection;
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel { Media: UserActivity activity })
            App.NavigateFrame(typeof(DetailsPage), activity.Media.Id);
    }

}
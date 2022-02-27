using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Common.Collections;
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
        var incrementalCollection = new IncrementalLoadingCollection<Source, MediaItemModel>(new Source(user.Id), 50);
        incrementalCollection.OnStartLoading += () => ActivityListIndicator.IsActive = true;
        incrementalCollection.OnEndLoading += () => ActivityListIndicator.IsActive = false;
        ActivityList.ItemsSource = incrementalCollection;
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel { Media: UserActivity activity })
            App.NavigateFrame(typeof(DetailsPage), activity.Media.Id);
    }

    public class Source : IIncrementalSource<MediaItemModel>
    {

        private readonly int _id;

        public Source(int id)
        {
            _id = id;
        }

        public async Task<IEnumerable<MediaItemModel>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = new())
        {
            return (await App.Client.GetUserActivities(_id, new AniPaginationOptions(pageIndex + 1, pageSize))).Data.Select(activity => new MediaItemModel(activity));
        }

    }

}
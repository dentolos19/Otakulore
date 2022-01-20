using System;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Web.WebView2.Core;
using Otakulore.Core;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class CinemaPage
{

    public CinemaPage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not PageParameter parameter)
            return;
        await WebView.EnsureCoreWebView2Async();
        var contents = parameter.Provider switch
        {
            IAnimeProvider provider => provider.GetAnimeEpisodes(parameter.MediaSource),
            IMangaProvider provider => provider.GetMangaChapters(parameter.MediaSource)
        };
        foreach (var content in contents)
            ContentList.Items.Add(new ContentItemModel(content));
        ContentList.SelectedIndex = 0;
    }

    private void OnContentChanged(object sender, SelectionChangedEventArgs args)
    {
        if (ContentList.SelectedItem is ContentItemModel item)
            WebView.CoreWebView2.Navigate(item.Content.Url);
    }

    private void OnContentNavigating(WebView2 sender, CoreWebView2NavigationStartingEventArgs args)
    {
        if (ContentList.Items[0] is not ComboBoxItem)
            ContentList.SelectedItem = ContentList.Items.Cast<ContentItemModel>().FirstOrDefault(item => item.Content.Url == args.Uri);
    }

}
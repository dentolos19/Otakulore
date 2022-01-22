using System;
using System.Collections.Generic;
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
        if (args.Parameter is not KeyValuePair<IProvider, object>(var provider, MediaSource source))
            return;
        await WebView.EnsureCoreWebView2Async();
        var contents = provider switch
        {
            IAnimeProvider animeProvider => animeProvider.GetAnimeEpisodes(source),
            IMangaProvider mangaProvider => mangaProvider.GetMangaChapters(source)
        };
        foreach (var content in contents)
            ContentSelection.Items.Add(new ContentItemModel(content));
        ContentSelection.SelectedIndex = 0;
    }

    private void OnContentChanged(object sender, SelectionChangedEventArgs args)
    {
        if (ContentSelection.SelectedItem is ContentItemModel item)
            WebView.CoreWebView2.Navigate(item.Content.Url);
    }

    private void OnContentNavigating(WebView2 sender, CoreWebView2NavigationStartingEventArgs args)
    {
        ContentSelection.SelectedItem = ContentSelection.Items.Cast<ContentItemModel>().FirstOrDefault(item => item.Content.Url == args.Uri);
    }

}
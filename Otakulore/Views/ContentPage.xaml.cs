using Microsoft.Web.WebView2.Core;
using Otakulore.Core;
using Otakulore.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Otakulore.Views;

public partial class ContentPage
{

    private readonly BackgroundWorker _contentLoader;

    private IProvider _provider;

    public ContentPage(IProvider provider, MediaSource source)
    {
        _contentLoader = new BackgroundWorker();
        _contentLoader.DoWork += (_, _) =>
        {
            try
            {
                var contents = provider switch
                {
                    IAnimeProvider provider => provider.GetAnimeEpisodes(source),
                    IMangaProvider provider => provider.GetMangaChapters(source)
                };
                Dispatcher.Invoke(() =>
                {
                    ContentSelection.Items.Clear();
                    foreach (var content in contents)
                        ContentSelection.Items.Add(new ContentItemModel(content));
                    ContentSelection.SelectedIndex = 0;
                });
            }
            catch
            {
                // TODO: notify user of exception and send them back
            }
        };
        _provider = provider;
        InitializeComponent();
        _contentLoader.RunWorkerAsync();
    }

    private void OnContentSelected(object sender, SelectionChangedEventArgs args)
    {
        if (ContentSelection.SelectedItem is not ContentItemModel item)
            return;
        if (item.Content.IsUrlVideo == false && _provider is IAnimeProvider animeProvider)
        {
            try
            {
                item.Content.Url = animeProvider.ExtractVideoUrl(item.Content);
                item.Content.IsUrlVideo = true;
            }
            catch
            {
                // do nothing
            }
        }
        WebView.CoreWebView2.Navigate(item.Content.Url.ToString());
    }

    private void OnPrevious(object sender, RoutedEventArgs args)
    {
        if (ContentSelection.SelectedIndex > 0)
            ContentSelection.SelectedIndex--;
    }

    private void OnNext(object sender, RoutedEventArgs args)
    {
        if (ContentSelection.SelectedIndex < ContentSelection.Items.Count - 1)
            ContentSelection.SelectedIndex++;
    }

    private void OnNavigating(object? sender, CoreWebView2NavigationStartingEventArgs args)
    {
        if (ContentSelection.Items[0] is ComboBoxItem)
            return;
        ContentSelection.SelectedItem = ContentSelection.Items.OfType<ContentItemModel>().FirstOrDefault(item => item.Content.Url == new Uri(args.Uri));
        PreviousButton.IsEnabled = ContentSelection.SelectedIndex > 0;
        NextButton.IsEnabled = ContentSelection.SelectedIndex < ContentSelection.Items.Count - 1;
    }

}
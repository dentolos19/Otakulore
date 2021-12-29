using Otakulore.Core;
using Otakulore.Models;
using System.ComponentModel;
using System.Windows.Controls;

namespace Otakulore.Views;

public partial class ContentPage
{

    private readonly BackgroundWorker _contentLoader;

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
                    foreach (var content in contents)
                        ContentList.Items.Add(new ContentItemModel(content));
                    ContentList.SelectedIndex = 0;
                });
            }
            catch
            {
                // TODO: notify user of exception and send them back
            }
        };
        InitializeComponent();
        _contentLoader.RunWorkerAsync();
    }

    private void OnContentSelected(object sender, SelectionChangedEventArgs args)
    {
        if (ContentList.SelectedItem is ContentItemModel item)
            WebView.CoreWebView2.Navigate(item.Content.Url.ToString());
    }

}
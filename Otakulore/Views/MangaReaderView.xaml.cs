using System;
using System.ComponentModel;
using System.Threading;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core.Helpers;
using Otakulore.Core.Services.Manga;
using Otakulore.Models;
using Otakulore.ViewModels;

namespace Otakulore.Views
{

    public sealed partial class MangaReaderView
    {
        
        private IMangaProvider _provider;

        public MangaReaderView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is ChannelItemModel model))
                return;
            _provider = model.MangaProvider;
            DataContext = PlayerReaderViewModel.CreateViewModel(model);
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                var chapterList = _provider.ScrapeMangaChapters(model.Url);
                if (chapterList != null && chapterList.Length > 0)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        foreach (var episode in chapterList)
                        {
                            ((PlayerReaderViewModel)DataContext).EpisodeChapterList.Add(EpisodeChapterItemModel.CreateModel(episode));
                        }
                    });
                }
                else
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        if (Frame.CanGoBack)
                            Frame.GoBack();
                        await new MessageDialog("Unable to scrape chapters with the current provider.").ShowAsync();
                    });
                    return;
                }
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    ((PlayerReaderViewModel)DataContext).IsLoading = false;
                    ReadMode.SelectedIndex = 0;
                    ChapterList.SelectedIndex = 0;
                });
            });
        }

        private void ModeChanged(object sender, SelectionChangedEventArgs args)
        {
            if (!(ReadMode.SelectedItem is ComboBoxItem item))
                return;
            switch (item.Tag.ToString())
            {
                case "mg":
                    MangaViewer.Visibility = Visibility.Visible;
                    WebtoonViewer.Visibility = Visibility.Collapsed;
                    break;
                case "wt":
                    MangaViewer.Visibility = Visibility.Collapsed;
                    WebtoonViewer.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void ChapterChanged(object sender, SelectionChangedEventArgs args)
        {
            if (!(ChapterList.SelectedItem is EpisodeChapterItemModel model))
                return;
            ((PlayerReaderViewModel)DataContext).IsLoading = true;
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                var chapterSources = _provider.ScrapeChapterSources(model.Url);
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    if (chapterSources != null && chapterSources.Length > 0)
                    {
                        ((PlayerReaderViewModel)DataContext).ChapterSourceList.Clear();
                        foreach (var source in chapterSources)
                            ((PlayerReaderViewModel)DataContext).ChapterSourceList.Add(new ChapterSourceItemModel { Url = source });
                    }
                    else
                    {
                        await new MessageDialog("Unable to scrape chapter with the current provider.").ShowAsync();
                    }
                    ((PlayerReaderViewModel)DataContext).IsLoading = false;
                });
            });
        }

        private void SetZoomFactor(object sender, RoutedEventArgs args)
        {
            ((ScrollViewer)sender).MinZoomFactor = 0.1f;
            ((ScrollViewer)sender).ChangeView(null, null, App.Settings.DefaultZoomFactor);
        }

    }

}
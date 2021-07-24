using Otakulore.Core.Services.Manga;
using Otakulore.Models;
using Otakulore.ViewModels;
using System;
using System.Threading;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
                    ChapterList.SelectedIndex = 0;
                });
            });
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

    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Navigation;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Services;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class MangaReaderPage
{

    private readonly BackgroundWorker _contentLoader;
    private readonly BackgroundWorker _sourcesLoader;

    private IMangaProvider? _provider;

    private MangaReaderViewModel ViewModel => (MangaReaderViewModel)DataContext;

    public MangaReaderPage()
    {
        _contentLoader = new BackgroundWorker();
        _sourcesLoader = new BackgroundWorker { WorkerSupportsCancellation = true };
        _contentLoader.DoWork += (_, args) =>
        {
            if (args.Argument is not IMediaInfo info)
                return;
            try
            {
                var chapters = _provider.GetMangaChapters(info);
                Dispatcher.Invoke(() =>
                {
                    foreach (var chapter in chapters)
                        ViewModel.Chapters.Add(ContentItemModel.Create(chapter));
                    ChapterSelection.SelectedIndex = 0;
                    ViewModel.HasLoaded = true;
                });
            }
            catch (Exception exception)
            {
                Dispatcher.Invoke(async () => await Utilities.CreateExceptionDialog(exception, "The provider returned an exception!").ShowAsync());
            }
        };
        _sourcesLoader.DoWork += (_, args) =>
        {
            if (args.Argument is not IMediaContent content)
                return;
            Dispatcher.Invoke(() => ViewModel.HasLoaded = false);
            try
            {
                var imageSources = _provider.GetMangaChapterSource(content);
                Dispatcher.Invoke(() =>
                {
                    ViewModel.ImageSources.Clear();
                    foreach (var imageSource in imageSources)
                        ViewModel.ImageSources.Add(new StringItemModel { Value = imageSource });
                    ViewModel.HasLoaded = true;
                });
            }
            catch (Exception exception)
            {
                Dispatcher.Invoke(async () => await Utilities.CreateExceptionDialog(exception, "The provider returned an exception!").ShowAsync());
            }
        };
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.ExtraData is not ObjectData data)
            return;
        ViewModel.Title = data.MediaInfo.Title;
        _provider = (IMangaProvider)data.Provider;
        _contentLoader.RunWorkerAsync(data.MediaInfo);
    }

    private void OnChapterChanged(object sender, SelectionChangedEventArgs args)
    {
        if (ChapterSelection.SelectedItem is not ContentItemModel content)
            return;
        _sourcesLoader.CancelAsync();
        _sourcesLoader.RunWorkerAsync(content.Content);
    }

}
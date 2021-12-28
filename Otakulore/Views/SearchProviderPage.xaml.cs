using Otakulore.Core;
using Otakulore.Models;
using Otakulore.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace Otakulore.Views;

public partial class SearchProviderPage
{

    private readonly BackgroundWorker _searcher;

    private string _query;

    public SearchProviderViewModel ViewModel => (SearchProviderViewModel)DataContext;

    public SearchProviderPage(IProvider provider, string? query = null)
    {
        _searcher = new BackgroundWorker { WorkerSupportsCancellation = true };
        _searcher.DoWork += (_, _) =>
        {
            Dispatcher.Invoke(() =>
            {
                ViewModel.HasSearched = false;
                SearchList.Items.Clear();
            });
            if (provider is IAnimeProvider animeProvider)
            {
                try
                {
                    var results = animeProvider.SearchAnime(_query);
                    Dispatcher.Invoke(() =>
                    {
                        foreach (var result in results)
                            SearchList.Items.Add(new SourceItemModel(MediaType.Anime, result));
                        ViewModel.HasSearched = true;
                    });
                }
                catch
                {
                    // do nothing
                }
            }
            else if (provider is IMangaProvider mangaProvider)
            {
                try
                {
                    var results = mangaProvider.SearchManga(_query);
                    Dispatcher.Invoke(() =>
                    {
                        foreach (var result in results)
                            SearchList.Items.Add(new SourceItemModel(MediaType.Manga, result));
                        ViewModel.HasSearched = true;
                    });
                }
                catch
                {
                    // do nothing
                }
            }
        };
        InitializeComponent();
        ViewModel.ImageUrl = provider.ImageUrl;
        if (string.IsNullOrEmpty(query))
            return;
        _query = query;
        SearchInput.Text = _query;
        _searcher.RunWorkerAsync();
    }

    private void OnSearch(object sender, RoutedEventArgs args)
    {
        _query = SearchInput.Text;
        if (_query is not { Length: > 3 })
            return;
        _searcher.CancelAsync();
        _searcher.RunWorkerAsync();
    }

}
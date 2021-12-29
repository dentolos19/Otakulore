using Otakulore.Core;
using Otakulore.Models;
using Otakulore.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Otakulore.Views;

public partial class SearchProviderPage
{

    private readonly BackgroundWorker _searcher;

    private string _query;
    private IProvider _provider;

    public SearchProviderViewModel ViewModel => (SearchProviderViewModel)DataContext;

    public SearchProviderPage(IProvider provider, string? query = null)
    {
        _searcher = new BackgroundWorker { WorkerSupportsCancellation = true };
        _searcher.DoWork += (_, _) =>
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    ViewModel.HasSearched = false;
                    SearchList.Items.Clear();
                });
                if (provider is IAnimeProvider animeProvider)
                {
                    var results = animeProvider.SearchAnime(_query);
                    Dispatcher.Invoke(() =>
                    {
                        foreach (var result in results)
                            SearchList.Items.Add(new SourceItemModel(result));
                        ViewModel.HasSearched = true;
                    });
                }
                else if (provider is IMangaProvider mangaProvider)
                {
                    var results = mangaProvider.SearchManga(_query);
                    Dispatcher.Invoke(() =>
                    {
                        foreach (var result in results)
                            SearchList.Items.Add(new SourceItemModel(result));
                        ViewModel.HasSearched = true;
                    });
                }
            }
            catch
            {
                // do nothing
            }
        };
        _provider = provider;
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
        if (string.IsNullOrEmpty(_query))
            return;
        _searcher.CancelAsync();
        _searcher.RunWorkerAsync();
    }

    private void OnOpenSource(object sender, MouseButtonEventArgs args)
    {
        if (SearchList.SelectedItem is SourceItemModel item)
            NavigationService.Navigate(new ContentPage(_provider, item.Source));
    }

}
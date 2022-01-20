using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Models;

namespace Otakulore.Views.Dialogs;

public sealed partial class SearchProviderDialog
{

    private readonly IProvider _provider;

    public MediaSource? Result { get; private set; }

    public SearchProviderDialog(IProvider provider, string? query = null)
    {
        _provider = provider;
        XamlRoot = App.MainWindow.Content.XamlRoot;
        InitializeComponent();
        if (!string.IsNullOrEmpty(query))
            SearchInput.Text = query;
    }

    private void Search(string query)
    {
        SearchResultList.Items.Clear();
        switch (_provider)
        {
            case IAnimeProvider provider:
            {
                var result = provider.SearchAnime(query);
                foreach (var entry in result)
                    SearchResultList.Items.Add(new SourceItemModel(entry));
                break;
            }
            case IMangaProvider provider:
            {
                var result = provider.SearchManga(query);
                foreach (var entry in result)
                    SearchResultList.Items.Add(new SourceItemModel(entry));
                break;
            }
        }
    }

    private void OnDialogOpened(ContentDialog sender, ContentDialogOpenedEventArgs args)
    {
        var query = SearchInput.Text;
        if (!string.IsNullOrEmpty(query))
            Search(query);
    }

    private void OnSearchRequested(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        var query = args.QueryText;
        if (!string.IsNullOrEmpty(query))
            Search(query);
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is not SourceItemModel item)
            return;
        Result = item.Source;
        Hide();
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

}
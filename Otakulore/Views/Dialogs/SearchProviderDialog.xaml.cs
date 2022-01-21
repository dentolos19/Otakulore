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

    private void Search()
    {
        var query = SearchInput.Text;
        if (string.IsNullOrEmpty(query))
            return;
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
        Search();
    }

    private void OnSearchRequested(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        Search();
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
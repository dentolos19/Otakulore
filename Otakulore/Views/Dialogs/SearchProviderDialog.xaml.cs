using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Controls;
using Otakulore.Core;
using Otakulore.Models;

namespace Otakulore.Views.Dialogs;

public sealed partial class SearchProviderDialog
{

    private readonly IProvider _provider;

    public SearchProviderDialog(IProvider provider, string? query = null)
    {
        _provider = provider;
        XamlRoot = App.Window.Content.XamlRoot;
        InitializeComponent();
        if (!string.IsNullOrEmpty(query))
            SearchInput.Text = query;
    }

    private void Search()
    {
        var query = SearchInput.Text;
        if (string.IsNullOrEmpty(query))
            return;
        SearchResultIndicator.State = ResultIndicatorState.Loading;
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
        SearchResultIndicator.State = SearchResultList.Items.Count > 0 ? ResultIndicatorState.None : ResultIndicatorState.NoResult;
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
        App.NavigateMainContent(typeof(CinemaPage), new KeyValuePair<IProvider, object>(_provider, item.Source));
        Hide();
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

}
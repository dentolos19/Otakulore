using System.Collections.Generic;
using System.Linq;
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
        ResultIndicator.State = ResultIndicatorState.Loading;
        var results = new List<SourceItemModel>();
        switch (_provider)
        {
            case IAnimeProvider animeProvider:
            {
                var sources = animeProvider.GetSources(query);
                results.AddRange(sources.Select(entry => new SourceItemModel(entry)));
                break;
            }
            case IMangaProvider mangaProvider:
            {
                var sources = mangaProvider.GetSources(query);
                results.AddRange(sources.Select(entry => new SourceItemModel(entry)));
                break;
            }
        }
        ResultIndicator.State = ResultIndicatorState.None;
        ResultList.Items.Clear();
        foreach (var entry in results)
            ResultList.Items.Add(entry);
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
        App.NavigateContent(typeof(CinemaPage), new KeyValuePair<IProvider, object>(_provider, item.Source));
        Hide();
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

}
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Views.Pages;

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
            SearchInputBox.Text = query;
    }

    public void Search()
    {
        var query = SearchInputBox.Text;
        if (string.IsNullOrEmpty(query))
            return;
        SearchResultList.Items.Clear();
        SearchProgressIndicator.IsActive = true;
        var results = new List<SourceItemModel>();
        switch (_provider)
        {
            case IAnimeProvider provider:
            {
                var sources = provider.GetSources(query);
                results.AddRange(sources.Select(entry => new SourceItemModel(entry)));
                break;
            }
            case IMangaProvider provider:
            {
                var sources = provider.GetSources(query);
                results.AddRange(sources.Select(entry => new SourceItemModel(entry)));
                break;
            }
        }
        SearchProgressIndicator.IsActive = false;
        foreach (var entry in results)
            SearchResultList.Items.Add(entry);
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is not SourceItemModel item)
            return;
        App.NavigateFrame(typeof(CinemaPage), new KeyValuePair<IProvider, object>(_provider, item.Source));
        Hide();
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

}
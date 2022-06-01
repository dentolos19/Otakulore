using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
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
        InitializeComponent();
        if (!string.IsNullOrEmpty(query))
            SearchInputBox.Text = query;
        SearchCommand.Execute(null);
    }

    private async void OnSearch(XamlUICommand sender, ExecuteRequestedEventArgs args)
    {
        var query = SearchInputBox.Text;
        if (string.IsNullOrEmpty(query))
            return;
        SearchResultList.Items.Clear();
        SearchProgressIndicator.IsActive = true;
        var results = await Task.Run(() => _provider.GetSources(query));
        foreach (var entry in results)
            SearchResultList.Items.Add(new SourceItemModel(entry));
        SearchProgressIndicator.IsActive = false;
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is not SourceItemModel item)
            return;
        App.NavigateFrame(typeof(CinemaPage), new KeyValuePair<IProvider, object>(_provider, item.Data));
        Hide();
    }

}
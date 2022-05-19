using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Web.WebView2.Core;
using Otakulore.Core;
using Otakulore.Models;

namespace Otakulore.Views.Pages;

public sealed partial class CinemaPage
{

    private IProvider _provider;

    public CinemaPage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not KeyValuePair<IProvider, object>(var provider, MediaSource source))
            return;
        _provider = provider;
        await WebView.EnsureCoreWebView2Async();
        WebView.CoreWebView2.NewWindowRequested += (_, args) => args.Handled = true;
        var contents = _provider.GetContents(source);
        foreach (var content in contents)
            ContentDropdown.Items.Add(new ContentItemModel(content));
        ContentDropdown.SelectedIndex = 0;
    }

    protected override void OnNavigatedFrom(NavigationEventArgs args)
    {
        WebView.Close();
    }

    private async void OnContentChanged(object sender, SelectionChangedEventArgs args)
    {
        if (ContentDropdown.SelectedItem is not ContentItemModel item)
            return;
        LoadingIndicator.IsLoading = true;
        var url = item.Content.Url;
        if (_provider is IAnimeProvider provider)
            url = await Task.Run(() => provider.TryExtractVideoPlayerUrl(item.Content, out var url) ? url : url);
        WebView.CoreWebView2.Navigate(url);
    }

    private void OnContentNavigated(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
    {
        LoadingIndicator.IsLoading = false;
    }

    private void OnNavigateBack(XamlUICommand sender, ExecuteRequestedEventArgs args)
    {
        ContentDropdown.SelectedIndex--;
    }

    private void OnNavigateForward(XamlUICommand sender, ExecuteRequestedEventArgs args)
    {
        ContentDropdown.SelectedIndex++;
    }

    private void CanNavigateBack(XamlUICommand sender, CanExecuteRequestedEventArgs args)
    {
        if (IsLoaded)
            args.CanExecute = ContentDropdown.SelectedIndex > 0;
    }

    private void CanNavigateForward(XamlUICommand sender, CanExecuteRequestedEventArgs args)
    {
        if (IsLoaded)
            args.CanExecute = ContentDropdown.SelectedIndex < ContentDropdown.Items.Count;
    }

}
using System;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Otakulore.Views;

public sealed partial class MainPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        NavigationView.SelectedItem = NavigationView.MenuItems.First();
    }

    private void OnSearchRequested(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        var query = sender.Text;
        if (!string.IsNullOrEmpty(query))
            ContentView.Navigate(typeof(SearchPage), query);
    }

    private void OnItemNavigate(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            ContentView.Navigate(typeof(SettingsPage));
        }
        else
        {
            if (args.SelectedItem is not NavigationViewItem item)
                return;
            var type = Type.GetType("Otakulore.Views." + item.Tag);
            if (type != null)
                ContentView.Navigate(type);
        }
    }

    private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        if (ContentView.CanGoBack)
            ContentView.GoBack();
    }

}
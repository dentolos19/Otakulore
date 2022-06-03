using System;
using System.Linq;
using AniListNet.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Views.Pages;

namespace Otakulore.Views;

public sealed partial class MainView
{

    public MainView()
    {
        InitializeComponent();
        NavigationView.MenuItems.Add(new NavigationViewItem { Icon = new SymbolIcon(Symbol.Home), Content = "Home", Tag = typeof(HomePage) });
        NavigationView.MenuItems.Add(new NavigationViewItem { Icon = new SymbolIcon(Symbol.Calendar), Content = "Schedules", Tag = typeof(SchedulesPage) });
        NavigationView.FooterMenuItems.Add(new NavigationViewItem { Icon = new SymbolIcon(Symbol.Contact), Content = "Profile", Tag = typeof(ProfilePage) });
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        NavigationView.SelectedItem = NavigationView.MenuItems.First();
    }

    private void OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            ContentView.Navigate(typeof(SettingsPage));
        }
        else
        {
            if (args.SelectedItem is NavigationViewItem { Tag: Type type })
                ContentView.Navigate(type);
        }
    }

    private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        if (ContentView.CanGoBack)
            ContentView.GoBack();
    }

    private void OnSearchEntered(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        ContentView.Navigate(typeof(SearchPage), new SearchMediaFilter { Query = sender.Text });
        NavigationView.IsPaneOpen = false;
    }

    private void OnPageNavigating(object sender, NavigatingCancelEventArgs args)
    {
        if (NavigationView.SelectedItem is NavigationViewItem { Tag: Type type } && type != args.SourcePageType)
            NavigationView.SelectedItem = null;
    }

}
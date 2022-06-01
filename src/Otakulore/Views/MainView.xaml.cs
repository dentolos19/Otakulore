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
        PageNavigation.MenuItems.Add(new NavigationViewItem { Icon = new SymbolIcon(Symbol.Home), Content = "Home", Tag = typeof(HomePage) });
        PageNavigation.MenuItems.Add(new NavigationViewItem { Icon = new SymbolIcon(Symbol.Calendar), Content = "Schedules", Tag = typeof(SchedulesPage) });
        PageNavigation.FooterMenuItems.Add(new NavigationViewItem { Icon = new SymbolIcon(Symbol.Contact), Content = "Profile", Tag = typeof(ProfilePage) });
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        PageNavigation.SelectedItem = PageNavigation.MenuItems.First();
    }

    private void OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            PageFrame.Navigate(typeof(SettingsPage));
        }
        else
        {
            if (args.SelectedItem is NavigationViewItem { Tag: Type type })
                PageFrame.Navigate(type);
        }
    }

    private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        if (PageFrame.CanGoBack)
            PageFrame.GoBack();
    }

    private void OnSearchEntered(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        PageFrame.Navigate(typeof(SearchPage), new SearchMediaFilter { Query = sender.Text });
        PageNavigation.IsPaneOpen = false;
    }

    private void OnPageNavigating(object sender, NavigatingCancelEventArgs args)
    {
        if (PageNavigation.SelectedItem is NavigationViewItem { Tag: Type type } && type != args.SourcePageType)
            PageNavigation.SelectedItem = null;
    }

}
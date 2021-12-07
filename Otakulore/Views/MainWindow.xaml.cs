using ModernWpf.Controls;
using System;
using System.Linq;
using System.Windows.Navigation;
using Otakulore.Core;

namespace Otakulore.Views;

public partial class MainWindow
{

    public MainWindow()
    {
        InitializeComponent();
        SideBar.SelectedItem = SideBar.MenuItems.OfType<NavigationViewItem>().First();
    }

    private void OnSearch(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        var query = SearchInput.Text;
        if (!string.IsNullOrEmpty(query))
            ContentView.Navigate(typeof(SearchPage), new ObjectData { Query = query });
    }

    private void OnNavigateView(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (SideBar.SelectedItem is not NavigationViewItem item)
            return;
        var type = Type.GetType("Otakulore.Views." + item.Tag);
        if (type is not null)
            ContentView.Navigate(type);
    }

    private void OnViewNavigated(object sender, NavigationEventArgs args)
    {
        SideBar.IsBackEnabled = ContentView.CanGoBack;
        if (SideBar.SelectedItem is not NavigationViewItem item)
            return;
        var type = Type.GetType($"Otakulore.Views.{item.Tag}");
        SideBar.SelectedItem = ContentView.CurrentSourcePageType == type;
    }

    private void OnBack(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        if (ContentView.CanGoBack)
            ContentView.GoBack();
    }

}
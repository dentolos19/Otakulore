using System;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Otakulore.Views
{
    
    public sealed partial class MainView
    {

        public MainView()
        {
            InitializeComponent();
            NavigationView.SelectedItem = NavigationView.MenuItems.OfType<NavigationViewItem>().First();
        }

        private void SwitchView(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingsView));
            }
            else
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                var viewType = Type.GetType("Otakulore.Views." + (string)selectedItem.Tag);
                if (viewType == null)
                    return;
                ContentFrame.Navigate(viewType);
            }
        }

        private void BackView(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
                ContentFrame.GoBack();
        }

        private void SearchEntered(object sender, KeyRoutedEventArgs args)
        {
            if (args.Key != VirtualKey.Enter)
                return;
            var query = SearchInput.Text;
            if (string.IsNullOrEmpty(query))
                return;
            ContentFrame.Navigate(typeof(SearchView), query);
        }

        private void ViewNavigated(object sender, NavigationEventArgs args)
        {
            NavigationView.IsBackEnabled = ContentFrame.CanGoBack;
        }

    }

}
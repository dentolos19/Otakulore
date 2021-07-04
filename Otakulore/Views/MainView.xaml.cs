using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using Windows.System;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Otakulore.Views
{

    public sealed partial class MainView
    {

        public MainView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            NavigationControl.SelectedItem = NavigationControl.MenuItems.OfType<NavigationViewItem>().First();
        }

        private void SwitchView(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingsView));
            }
            else
            {
                if (!(NavigationControl.SelectedItem is NavigationViewItem item))
                    return;
                var type = Type.GetType("Otakulore.Views." + item.Tag);
                if (type == null)
                    return;
                ContentFrame.Navigate(type);
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
            NavigationControl.IsBackEnabled = ContentFrame.CanGoBack;
            if (!(NavigationControl.SelectedItem is NavigationViewItem item))
                return;
            var type = Type.GetType($"Otakulore.Views.{item.Tag}");
            if (args.SourcePageType != type)
                NavigationControl.SelectedItem = null;
        }

    }

}
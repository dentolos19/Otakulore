using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using NavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
using NavigationViewBackRequestedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs;
using NavigationViewItem = Microsoft.UI.Xaml.Controls.NavigationViewItem;
using NavigationViewSelectionChangedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs;

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
        
        private void ViewNavigated(object sender, NavigationEventArgs args)
        {
            NavigationControl.IsBackEnabled = ContentFrame.CanGoBack;
            if (!(NavigationControl.SelectedItem is NavigationViewItem item))
                return;
            var type = Type.GetType($"Otakulore.Views.{item.Tag}");
            if (args.SourcePageType != type)
                NavigationControl.SelectedItem = null;
        }

        private void SearchEntered(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var query = SearchInput.Text;
            if (string.IsNullOrEmpty(query))
                return;
            ContentFrame.Navigate(typeof(SearchView), query);
        }

    }

}
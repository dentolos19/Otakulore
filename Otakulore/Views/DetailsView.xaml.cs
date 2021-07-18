using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core.Services.Common;
using NewNavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
using NewNavigationViewItem = Microsoft.UI.Xaml.Controls.NavigationViewItem;
using NewNavigationViewSelectionChangedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs;

namespace Otakulore.Views
{

    public sealed partial class DetailsView
    {

        private CommonMediaDetails _details;

        public DetailsView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is CommonMediaDetails details))
                return;
            _details = details;
            if (_details.MediaType == CommonMediaType.Anime)
            {
                NavigationControl.MenuItems.Add(new NewNavigationViewItem
                {
                    Content = "Watch",
                    Tag = nameof(DetailsWatchView),
                    Icon = new FontIcon { Glyph = "\xE116" }
                });
            }
            else
            {
                NavigationControl.MenuItems.Add(new NewNavigationViewItem
                {
                    Content = "Read",
                    Tag = nameof(DetailsReadView),
                    Icon = new FontIcon { Glyph = "\xE79E" }
                });
            }
            NavigationControl.SelectedItem = NavigationControl.MenuItems.OfType<NewNavigationViewItem>().First();
        }

        private void SwitchView(NewNavigationView sender, NewNavigationViewSelectionChangedEventArgs args)
        {
            if (!(NavigationControl.SelectedItem is NewNavigationViewItem item))
                return;
            var type = Type.GetType("Otakulore.Views." + item.Tag);
            if (type == null)
                return;
            ContentFrame.Navigate(type, _details);
        }

        private void ViewNavigated(object sender, NavigationEventArgs args)
        {
            NavigationControl.IsBackEnabled = ContentFrame.CanGoBack;
            if (!(NavigationControl.SelectedItem is NewNavigationViewItem item))
                return;
            var type = Type.GetType($"Otakulore.Views.{item.Tag}");
            if (args.SourcePageType != type)
                NavigationControl.SelectedItem = null;
        }

    }

}
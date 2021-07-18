using System;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core.Services.Common;

namespace Otakulore.Views
{

    public sealed partial class DetailsView
    {

        private CommonMediaDetails _data;

        public DetailsView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is CommonMediaDetails data))
                return;
            _data = data;
            NavigationControl.SelectedItem = NavigationControl.MenuItems.OfType<NavigationViewItem>().First();
        }

        private void SwitchView(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (!(NavigationControl.SelectedItem is NavigationViewItem item))
                return;
            var type = Type.GetType("Otakulore.Views." + item.Tag);
            if (type == null)
                return;
            ContentFrame.Navigate(type, _data);
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
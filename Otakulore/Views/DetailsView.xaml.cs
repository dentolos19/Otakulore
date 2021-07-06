using System;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core.Services.Kitsu;

namespace Otakulore.Views
{

    public sealed partial class DetailsView
    {

        private KitsuData<KitsuAnimeAttributes> _data;

        public DetailsView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is KitsuData<KitsuAnimeAttributes> data))
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

    }

}
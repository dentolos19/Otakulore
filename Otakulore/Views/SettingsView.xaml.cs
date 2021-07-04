using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.Services.Anime;

namespace Otakulore.Views
{

    public sealed partial class SettingsView
    {

        public SettingsView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            foreach (var provider in CoreUtilities.GetAnimeProviders())
            {
                var providerItem = new ComboBoxItem
                {
                    Content = provider.ProviderName,
                    Tag = provider
                };
                DefaultProviderSelection.Items.Add(providerItem);
            }
            DefaultProviderSelection.SelectedItem = DefaultProviderSelection.Items.OfType<ComboBoxItem>().FirstOrDefault(item => ((IAnimeProvider)item.Tag).ProviderId == App.Settings.DefaultAnimeProvider);
            ShowEpisodeInfoSwitch.IsOn = App.Settings.ShowEpisodeInfo;
        }

        private void UpdateSettings(object sender, RoutedEventArgs args)
        {
            App.Settings.DefaultAnimeProvider = ((IAnimeProvider)((ComboBoxItem)DefaultProviderSelection.SelectedItem).Tag).ProviderId;
            App.Settings.ShowEpisodeInfo = ShowEpisodeInfoSwitch.IsOn;
        }

    }

}
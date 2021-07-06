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
            AboutText.Text = CoreUtilities.GetResourceFileAsString("About.txt");
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            foreach (var animeProvider in CoreUtilities.GetAnimeProviders())
            {
                var providerItem = new ComboBoxItem
                {
                    Content = animeProvider.Name,
                    Tag = animeProvider
                };
                DefaultAnimeProviderSelection.Items.Add(providerItem);
            }
            DefaultAnimeProviderSelection.SelectedItem = DefaultAnimeProviderSelection.Items.OfType<ComboBoxItem>().FirstOrDefault(item => ((IAnimeProvider)item.Tag).Id == App.Settings.DefaultAnimeProvider);
            ShowEpisodeInfoSwitch.IsOn = App.Settings.ShowEpisodeInfo;
        }

        private void UpdateSettings(object sender, RoutedEventArgs args)
        {
            App.Settings.DefaultAnimeProvider = ((IAnimeProvider)((ComboBoxItem)DefaultAnimeProviderSelection.SelectedItem).Tag).Id;
            App.Settings.ShowEpisodeInfo = ShowEpisodeInfoSwitch.IsOn;
        }

    }

}
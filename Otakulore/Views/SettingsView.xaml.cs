using System.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Anime.Providers;

namespace Otakulore.Views
{

    public sealed partial class SettingsView
    {

        public SettingsView()
        {
            InitializeComponent();
            var packageVersion = Package.Current.Id.Version;
            VersionText.Text = $"v{packageVersion.Major}.{packageVersion.Minor}";
            #if DEBUG
            VersionText.Text += "-DEBUG";
            #endif
            AboutText.Text = CoreUtilities.GetResourceFileAsString("About.txt");
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            foreach (var animeProvider in ServiceUtilities.GetAnimeProviders())
            {
                var providerItem = new ComboBoxItem
                {
                    Content = animeProvider.Name,
                    Tag = animeProvider
                };
                DefaultAnimeProviderSelection.Items.Add(providerItem);
            }
            DefaultAnimeProviderSelection.SelectedItem = DefaultAnimeProviderSelection.Items.OfType<ComboBoxItem>().FirstOrDefault(item => ((IAnimeProvider)item.Tag).Id == App.Settings.DefaultAnimeProvider);
        }

        private void UpdateSettings(object sender, RoutedEventArgs args)
        {
            App.Settings.DefaultAnimeProvider = ((IAnimeProvider)((ComboBoxItem)DefaultAnimeProviderSelection.SelectedItem).Tag).Id;
        }

    }

}
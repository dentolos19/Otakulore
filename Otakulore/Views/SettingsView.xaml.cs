using Otakulore.Core;
using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Manga;
using System;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
            AboutText.Text = CoreUtilities.GetEmbeddedResourceAsString("About.txt");
            foreach (var animeProvider in ServiceUtilities.GetAnimeProviders())
            {
                var providerItem = new ComboBoxItem
                {
                    Content = animeProvider.Name,
                    Tag = animeProvider
                };
                DefaultAnimeProviderSelection.Items.Add(providerItem);
            }
            foreach (var mangaProvider in ServiceUtilities.GetMangaProviders())
            {
                var providerItem = new ComboBoxItem
                {
                    Content = mangaProvider.Name,
                    Tag = mangaProvider
                };
                DefaultMangaProviderSelection.Items.Add(providerItem);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            DefaultAnimeProviderSelection.SelectedItem = DefaultAnimeProviderSelection.Items.OfType<ComboBoxItem>().FirstOrDefault(item => ((IAnimeProvider)item.Tag).Id == App.Settings.DefaultAnimeProvider);
            DefaultMangaProviderSelection.SelectedItem = DefaultMangaProviderSelection.Items.OfType<ComboBoxItem>().FirstOrDefault(item => ((IMangaProvider)item.Tag).Id == App.Settings.DefaultMangaProvider);
        }

        private void UpdateSettings(object sender, RoutedEventArgs args)
        {
            if (DefaultAnimeProviderSelection.SelectedItem != null)
                App.Settings.DefaultAnimeProvider = ((IAnimeProvider)((ComboBoxItem)DefaultAnimeProviderSelection.SelectedItem).Tag).Id;
            if (DefaultMangaProviderSelection.SelectedItem != null)
                App.Settings.DefaultMangaProvider = ((IMangaProvider)((ComboBoxItem)DefaultMangaProviderSelection.SelectedItem).Tag).Id;
        }

        private async void OpenLocalFolder(object sender, RoutedEventArgs args)
        {
            await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);
        }

        private async void OpenCacheFolder(object sender, RoutedEventArgs args)
        {
            await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalCacheFolder);
        }

    }

}
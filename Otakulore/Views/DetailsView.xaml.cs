using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Anime.Providers;
using Otakulore.Core.Services.Kitsu;
using Otakulore.Models;
using Otakulore.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core;

namespace Otakulore.Views
{

    public sealed partial class DetailsView
    {

        private readonly BackgroundWorker _genreLoader;
        private readonly BackgroundWorker _contentLoader;

        private string _id;

        public DetailsView()
        {
            InitializeComponent();
            _genreLoader = new BackgroundWorker();
            _genreLoader.DoWork += LoadGenres;
            _contentLoader = new BackgroundWorker();
            _contentLoader.DoWork += LoadContent;
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is KitsuData<KitsuAnimeAttributes> data))
                return;
            DataContext = DetailsViewModel.CreateViewModel(data);
            _id = data.Id;
            FavoriteButton.IsChecked = App.Settings.FavoriteList.Contains(_id);
            SearchInput.Text = data.Attributes.CanonicalTitle;
            var titles = new List<TitleItemModel>();
            foreach (var (languageCode, title) in data.Attributes.Titles)
            {
                if (string.IsNullOrEmpty(languageCode) || string.IsNullOrEmpty(title))
                    return;
                string languageName = null;
                try
                {
                    languageName = CultureInfo.GetCultureInfo(languageCode).DisplayName;
                }
                catch
                {
                    // do nothing
                }
                if (string.IsNullOrEmpty(languageName))
                    languageName = languageCode;
                titles.Add(new TitleItemModel
                {
                    LanguageName = languageName,
                    Title = title
                });
            }
            SearchInput.ItemsSource = titles;
            foreach (var provider in CoreUtilities.GetAnimeProviders())
            {
                var providerItem = new ComboBoxItem
                {
                    Content = provider.Name,
                    Tag = provider
                };
                ProviderSelection.Items.Add(providerItem);
            }
            _genreLoader.RunWorkerAsync();
        }

        private async void LoadGenres(object sender, DoWorkEventArgs args)
        {
            var genres = await KitsuApi.GetAnimeGenresAsync(_id);
            if (genres != null && genres.Length > 0)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    foreach (var genre in genres)
                        GenreList.Children.Add(new TextBlock
                        {
                            Text = genre.Attributes.Name
                        });
                });
            }
        }

        private async void LoadContent(object sender, DoWorkEventArgs args)
        {
            if (!(args.Argument is KeyValuePair<string, IAnimeProvider> parameters))
                return;
            var query = parameters.Key;
            var provider = parameters.Value;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ((DetailsViewModel)DataContext).IsLoading = true);
            var content = provider.ScrapeAnimes(query);
            if (content != null && content.Length > 0)
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    ContentList.Items.Clear();
                    foreach (var channel in content)
                        ContentList.Items.Add(new WatchItemModel
                        {
                            Id = _id,
                            ImageUrl = channel.ImageUrl,
                            Title = channel.Title,
                            EpisodesUrl = channel.EpisodesUrl,
                            Provider = provider
                        });
                });
            else
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await new MessageDialog("Unable to find available content with the current provider.").ShowAsync());
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ((DetailsViewModel)DataContext).IsLoading = false);
        }

        private void TabSwitched(object sender, SelectionChangedEventArgs args)
        {
            if (TabControl.SelectedIndex == 1 && ProviderSelection.SelectedIndex < 0)
                ProviderSelection.SelectedItem = ProviderSelection.Items.OfType<ComboBoxItem>().FirstOrDefault(item => ((IAnimeProvider)item.Tag).Id == App.Settings.DefaultAnimeProvider);
        }

        private void UpdateFavorites(object sender, RoutedEventArgs args)
        {
            if (FavoriteButton.IsChecked == true)
            {
                if (!App.Settings.FavoriteList.Contains(_id))
                    App.Settings.FavoriteList.Add(_id);
            }
            else
            {
                if (App.Settings.FavoriteList.Contains(_id))
                    App.Settings.FavoriteList.Remove(_id);
            }
        }

        private void SearchEntered(object sender, KeyRoutedEventArgs args)
        {
            if (args.Key != VirtualKey.Enter)
                return;
            UpdateContent(null, null);
        }

        private void ContentProviderSelected(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (!(args.SelectedItem is TitleItemModel model))
                return;
            sender.Text = model.Title;
            UpdateContent(null, null);
        }

        private async void UpdateContent(object sender, SelectionChangedEventArgs args)
        {
            if (!(ProviderSelection.SelectedItem is ComboBoxItem item))
                return;
            var query = SearchInput.Text;
            if (string.IsNullOrEmpty(query))
                return;
            if (item.Tag is IAnimeProvider provider)
            {
                _contentLoader.RunWorkerAsync(new KeyValuePair<string, IAnimeProvider>(query, provider));
            }
            else
            {
                ((DetailsViewModel)DataContext).IsLoading = false;
                await new MessageDialog("The selected provider is unavailable.").ShowAsync();
            }
        }

        private void WatchContent(object sender, ItemClickEventArgs args)
        {
            if (args.ClickedItem is WatchItemModel model)
                Frame.Navigate(typeof(WatchView), model);
        }

    }

}
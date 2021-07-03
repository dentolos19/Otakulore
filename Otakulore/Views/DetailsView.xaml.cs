using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Anime.Providers;
using Otakulore.Core.Services.Kitsu;
using Otakulore.Models;
using Otakulore.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
            ContentSearchInput.Text = data.Attributes.CanonicalTitle;
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
            ContentSearchInput.ItemsSource = titles;
            _genreLoader.RunWorkerAsync();
        }

        private async void LoadGenres(object sender, DoWorkEventArgs args) // TODO: redo genres
        {
            var genres = await KitsuApi.GetAnimeGenresAsync(_id);
            if (genres != null && genres.Length > 0)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    foreach (var genre in genres)
                    {
                        GenreStack.Children.Add(new Border
                        {
                            Background = new SolidColorBrush(Colors.DarkSlateGray),
                            CornerRadius = new CornerRadius(10),
                            Padding = new Thickness(10, 2, 10, 2),
                            Child = new TextBlock
                            {
                                Text = genre.Attributes.Name,
                                Foreground = new SolidColorBrush(Colors.White),
                                FontWeight = FontWeights.Bold
                            }
                        });
                    }
                });
            }
        }

        private async void LoadContent(object sender, DoWorkEventArgs args)
        {
            if (!(args.Argument is KeyValuePair<string, AnimeProvider> parameters))
                return;
            var query = parameters.Key;
            var provider = parameters.Value;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ((DetailsViewModel)DataContext).IsLoading = true);
            AnimeInfo[] content = null;
            switch (provider)
            {
                case AnimeProvider.AnimeKisa:
                    content = AnimeKisaProvider.ScrapeSearchAnime(query);
                    break;
                case AnimeProvider.Gogoanime:
                    content = GogoanimeProvider.ScrapeSearchAnime(query);
                    break;
                case AnimeProvider.FourAnime:
                    content = FourAnimeProvider.ScrapeSearchAnime(query);
                    break;
            }
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
            if (TabControl.SelectedIndex == 1 && ContentProviderSelection.SelectedIndex < 0)
                ContentProviderSelection.SelectedIndex = 0;
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

        private void ContentSearchEntered(object sender, KeyRoutedEventArgs args)
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
            if (!(ContentProviderSelection.SelectedItem is ComboBoxItem item))
                return;
            var query = ContentSearchInput.Text;
            if (string.IsNullOrEmpty(query))
                return;
            AnimeProvider provider;
            switch (item.Tag.ToString())
            {
                case "ak":
                    provider = AnimeProvider.AnimeKisa;
                    break;
                case "ggo":
                    provider = AnimeProvider.Gogoanime;
                    break;
                case "4a":
                    provider = AnimeProvider.FourAnime;
                    break;
                default:
                    ((DetailsViewModel)DataContext).IsLoading = false;
                    await new MessageDialog("The selected provider is unavailable.").ShowAsync();
                    return;
            }
            _contentLoader.RunWorkerAsync(new KeyValuePair<string, AnimeProvider>(query, provider));
        }

        private void WatchSelectedContent(object sender, ItemClickEventArgs args)
        {
            if (args.ClickedItem is WatchItemModel model)
                Frame.Navigate(typeof(WatchView), model);
        }

    }

}
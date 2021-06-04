using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Humanizer;
using Otakulore.Core;
using Otakulore.Core.Anime;
using Otakulore.Core.Anime.Providers;
using Otakulore.Core.Kitsu;
using Otakulore.Models;

namespace Otakulore.Views
{

    public partial class DetailsView
    {

        private readonly KitsuData _data;

        private bool _isFourAnimeLoaded;
        private bool _isGogoanimeLoaded;

        public DetailsView(KitsuData data)
        {
            InitializeComponent();
            _data = data;
            TitleText.Text = data.Attributes.CanonicalTitle;
            YearText.Text = data.Attributes.StartingDate?.Substring(0, 4) ?? "Unknown";
            FormatText.Text = data.Attributes.Format.Humanize();
            StatusText.Text = data.Attributes.Status.Transform(To.TitleCase);
            EpisodesText.Text = data.Attributes.EpisodeCount.HasValue ? data.Attributes.EpisodeCount.ToString() : "Unknown";
            StartingDateText.Text = data.Attributes.StartingDate ?? "TBA";
            EndingDateText.Text = data.Attributes.EndingDate ?? "Unknown";
            SynopsisText.Text = data.Attributes.Synopsis;
            FavoriteButton.Content = App.Settings.FavoritesList.Contains(_data.Id) ? "\xE00B" : "\xE006";
            FavoriteButton.ToolTip = App.Settings.FavoritesList.Contains(_data.Id) ? "Remove From Favorites" : "Add To Favorites";
            ThreadPool.QueueUserWorkItem(_ =>
            {
                using var client = new WebClient();
                var buffer = client.DownloadData(data.Attributes.PosterImage.OriginalImageUrl);
                var bitmap = new BitmapImage();
                using (var stream = new MemoryStream(buffer))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    bitmap.Freeze();
                }
                Dispatcher.BeginInvoke(() => PosterImage.Source = bitmap);
            });
        }

        private void ToggleFavorite(object sender, RoutedEventArgs args)
        {
            if (App.Settings.FavoritesList.Contains(_data.Id))
            {
                App.Settings.FavoritesList.Remove(_data.Id);
                App.Settings.SaveData();
                FavoriteButton.Content = "\xE006";
                FavoriteButton.ToolTip = "Add To Favorites";
            }
            else
            {
                App.Settings.FavoritesList.Add(_data.Id);
                App.Settings.SaveData();
                FavoriteButton.Content = "\xE00B";
                FavoriteButton.ToolTip = "Remove From Favorites";
            }
        }

        private void StreamFourAnime(object sender, MouseButtonEventArgs args)
        {
            if (FourAnimeList.SelectedItem is StreamItemModel model)
                App.NavigateSinglePage(new StreamDetailsView(model.Title, model.EpisodesUrl, model.Service));
        }

        private void StreamGogoanime(object sender, MouseButtonEventArgs args)
        {
            if (GogoanimeList.SelectedItem is StreamItemModel model)
                App.NavigateSinglePage(new StreamDetailsView(model.Title, model.EpisodesUrl, model.Service));
        }

        private void FourAnimeSectionExpanded(object sender, RoutedEventArgs args)
        {
            if (_isFourAnimeLoaded)
                return;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var posters = FourAnimeProvider.SearchAnime(_data.Attributes.CanonicalTitle);
                if (posters == null)
                {
                    Dispatcher.BeginInvoke(() => FourAnimeLoadingIndicator.Text = "Failed to scrape content.");
                    return;
                }
                if (posters.Length <= 0)
                {
                    Dispatcher.BeginInvoke(() => FourAnimeLoadingIndicator.Text = "No content found.");
                    return;
                }
                foreach (var poster in posters)
                {
                    Dispatcher.BeginInvoke(() => FourAnimeList.Items.Add(new StreamItemModel
                    {
                        ImageUrl = poster.ImageUrl,
                        Title = poster.Title,
                        Service = AnimeProvider.FourAnime,
                        EpisodesUrl = poster.EpisodesUrl
                    }));
                }
                Dispatcher.BeginInvoke(() => FourAnimeLoadingPanel.Visibility = Visibility.Collapsed);
                _isFourAnimeLoaded = true;
            });
        }

        private void GogoanimeSectionExpanded(object sender, RoutedEventArgs args)
        {
            if (_isGogoanimeLoaded)
                return;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var posters = GogoanimeProvider.SearchAnime(_data.Attributes.CanonicalTitle);
                if (posters == null)
                {
                    Dispatcher.BeginInvoke(() => GogoanimeLoadingIndicator.Text = "Failed to scrape content.");
                    return;
                }
                if (posters.Length <= 0)
                {
                    Dispatcher.BeginInvoke(() => GogoanimeLoadingIndicator.Text = "No content found.");
                    return;
                }
                foreach (var poster in posters)
                {
                    Dispatcher.BeginInvoke(() => GogoanimeList.Items.Add(new StreamItemModel
                    {
                        ImageUrl = poster.ImageUrl,
                        Title = poster.Title,
                        Service = AnimeProvider.Gogoanime,
                        EpisodesUrl = poster.EpisodesUrl
                    }));
                }
                Dispatcher.BeginInvoke(() => GogoanimeLoadingPanel.Visibility = Visibility.Collapsed);
                _isGogoanimeLoaded = true;
            });
        }

    }

}
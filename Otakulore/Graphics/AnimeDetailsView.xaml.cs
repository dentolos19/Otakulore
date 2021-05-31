using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Otakulore.Core.AnimeServices;
using Otakulore.Core.AnimeServices.Scrapers;
using Otakulore.Core.Kitsu;
using Otakulore.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace Otakulore.Graphics
{

    public partial class AnimeDetailsView
    {

        private readonly KitsuData _data;
        
        public AnimeDetailsView(KitsuData data)
        {
            InitializeComponent();
            _data = data;
            TitleText.Text = data.Attributes.CanonicalTitle;
            YearText.Text = data.Attributes.StartingDate?.Substring(0, 4) ?? "XXXX";
            FormatText.Text = data.Attributes.Format.ToString();
            StatusText.Text = data.Attributes.Status;
            SynopsisText.Text = data.Attributes.Synopsis;
            FavoriteButton.Content = App.Settings.FavoritesList.Contains(_data.Id) ? "\xE00B" : "\xE006";
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
            FourAnimeList.Items.Clear();
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var posters = FourAnimeScraper.SearchAnime(_data.Attributes.CanonicalTitle);
                if (posters == null)
                    return; // TODO: add null indicator
                foreach (var poster in posters)
                {
                    Dispatcher.BeginInvoke(() => FourAnimeList.Items.Add(new StreamItemModel
                    {
                        ImageUrl = poster.ImageUrl,
                        Title = poster.Title,
                        Service = StreamingService.FourAnime,
                        EpisodesUrl = poster.EpisodesUrl
                    }));
                }
            });
            GogoanimeList.Items.Clear();
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var posters = GogoanimeScraper.SearchAnime(_data.Attributes.CanonicalTitle);
                if (posters == null)
                    return; // TODO: add null indicator
                foreach (var poster in posters)
                {
                    Dispatcher.BeginInvoke(() => GogoanimeList.Items.Add(new StreamItemModel
                    {
                        ImageUrl = poster.ImageUrl,
                        Title = poster.Title,
                        Service = StreamingService.Gogoanime,
                        EpisodesUrl = poster.EpisodesUrl
                    }));
                }
            });
        }

        private void ToggleFavorite(object sender, RoutedEventArgs args)
        {
            if (App.Settings.FavoritesList.Contains(_data.Id))
            {
                App.Settings.FavoritesList.Remove(_data.Id);
                App.Settings.SaveData();
                FavoriteButton.Content = "\xE006";
            }
            else
            {
                App.Settings.FavoritesList.Add(_data.Id);
                App.Settings.SaveData();
                FavoriteButton.Content = "\xE00B";
            }
        }

        private void StreamFourAnime(object sender, MouseButtonEventArgs args)
        {
            if (FourAnimeList.SelectedItem is StreamItemModel model)
                App.NavigateSinglePage(new StreamDetailsView(model.EpisodesUrl, model.Service));
        }

        private void StreamGogoanime(object sender, MouseButtonEventArgs args)
        {
            AdonisMessageBox.Show("Sorry, this streaming service is currently disabled.", "Otakulore");
        }

    }

}
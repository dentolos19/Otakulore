using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Otakulore.Core.AnimeServices.Scrapers;
using Otakulore.Core.Kitsu;
using Otakulore.Models;

namespace Otakulore.Graphics
{

    public partial class AnimeDetailsView
    {

        private readonly KitsuData _data;
        
        public AnimeDetailsView(object data)
        {
            InitializeComponent();
            if (data is not KitsuData animeData)
                return;
            _data = animeData;
            TitleText.Text = animeData.Attributes.CanonicalTitle;
            YearText.Text = animeData.Attributes.StartingDate.Substring(0, 4);
            FormatText.Text = animeData.Attributes.Format.ToString();
            StatusText.Text = animeData.Attributes.Status.ToString();
            SynopsisText.Text = animeData.Attributes.Synopsis;
            FavoriteButton.Content = App.Settings.FavoritesList.Contains(_data.Id) ? "\xE00B" : "\xE006";
            ThreadPool.QueueUserWorkItem(_ =>
            {
                using var client = new WebClient();
                var buffer = client.DownloadData(animeData.Attributes.PosterImage.OriginalImageUrl);
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
            FourAnimeSection.Items.Clear();
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var posters = FourAnimeScraper.SearchAnime(_data.Attributes.CanonicalTitle);
                if (posters == null)
                    return; // TODO: add null indicator
                foreach (var poster in posters)
                {
                    Dispatcher.BeginInvoke(() => FourAnimeSection.Items.Add(new StreamItemModel
                    {
                        ImageUrl = poster.ImageUrl,
                        Title = poster.Title
                    }));
                }
            });
            GogoanimeSection.Items.Clear();
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var posters = GogoanimeScraper.SearchAnime(_data.Attributes.CanonicalTitle);
                if (posters == null)
                    return; // TODO: add null indicator
                foreach (var poster in posters)
                {
                    Dispatcher.BeginInvoke(() => GogoanimeSection.Items.Add(new StreamItemModel
                    {
                        ImageUrl = poster.ImageUrl,
                        Title = poster.Title
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

    }

}
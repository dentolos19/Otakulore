using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Media.Imaging;
using Otakulore.Core.Kitsu;

namespace Otakulore.Graphics
{

    public partial class DetailsView
    {

        private readonly WebClient _client = new();

        public DetailsView()
        {
            InitializeComponent();
        }

        public void ShowDetails(object data)
        {
            if (data is KitsuAnimeAttributes attributes)
            {
                TitleText.Text = attributes.CanonicalTitle;
                YearText.Text = attributes.StartingDate.Substring(0, 4);
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    var buffer = _client.DownloadData(attributes.PosterImage.OriginalImageUrl);
                    var image = new BitmapImage();
                    using (var stream = new MemoryStream(buffer))
                    {
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = stream;
                        image.EndInit();
                        image.Freeze();
                    }
                    Dispatcher.BeginInvoke((Action)(() => PosterImage.Source = image));
                });
            }
        }

    }

}
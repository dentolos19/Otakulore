using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Media.Imaging;
using Kitsu.Anime;

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
            if (data is AnimeAttributesModel attributes)
            {
                TitleText.Text = attributes.CanonicalTitle;
                YearText.Text = attributes.StartDate.Substring(0, 4);
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    var buffer = _client.DownloadData(attributes.PosterImage.Original);
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
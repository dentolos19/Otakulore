using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Otakulore.Core.Kitsu;

namespace Otakulore.Graphics
{

    public partial class DetailsView
    {
        
        public DetailsView()
        {
            InitializeComponent();
        }

        public void ShowDetails(object data)
        {
            if (data is not KitsuData animeData)
                return;
            TitleText.Text = animeData.Attributes.CanonicalTitle;
            YearText.Text = animeData.Attributes.StartingDate.Substring(0, 4);
            FormatText.Text = animeData.Attributes.Format.ToString();
            StatusText.Text = animeData.Attributes.Status.ToString();
            SynopsisText.Text = animeData.Attributes.Synopsis;
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
        }

    }

}
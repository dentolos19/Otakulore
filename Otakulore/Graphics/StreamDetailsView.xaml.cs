using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using Otakulore.Core.AnimeServices;
using Otakulore.Core.AnimeServices.Scrapers;
using Otakulore.Models;

namespace Otakulore.Graphics
{

    public partial class StreamDetailsView
    {

        private readonly string _url;
        private readonly StreamingService _service;
        private readonly BackgroundWorker _worker;

        public StreamDetailsView(string url, StreamingService service)
        {
            InitializeComponent();
            _url = url;
            _service = service;
            _worker = new BackgroundWorker();
            _worker.DoWork += LoadEpisodes;
            _worker.RunWorkerAsync();
        }

        private void LoadEpisodes(object sender, DoWorkEventArgs args)
        {
            switch (_service)
            {
                case StreamingService.FourAnime:
                {
                    var episodes = FourAnimeScraper.ScrapeEpisodes(_url);
                    if (episodes == null)
                        return; // TODO: add null indictator
                    foreach (var episode in episodes)
                    {
                        Dispatcher.BeginInvoke(() => EpisodeList.Items.Add(new EpisodeItemModel
                        {
                            EpisodeNumber = "Episode " + episode.EpisodeNumber,
                            WatchUrl = episode.WatchUrl
                        }));
                    }
                    break;
                }
                case StreamingService.Gogoanime:
                {
                    // TODO: add gogoanime streaming details
                    break;
                }
            }
        }

        private void PlayEpisode(object sender, MouseButtonEventArgs args)
        {
            if (EpisodeList.SelectedItem is EpisodeItemModel model)
                App.NavigateSinglePage(new MediaPlayerView(model.WatchUrl, _service));
        }

    }

}
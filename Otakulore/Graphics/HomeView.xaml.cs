using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using Otakulore.Core.Kitsu;
using Otakulore.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace Otakulore.Graphics
{

    public partial class HomeView
    {

        private readonly BackgroundWorker _worker;

        public HomeView()
        {
            InitializeComponent();
            _worker = new BackgroundWorker();
            _worker.DoWork += AddContent;
            _worker.RunWorkerAsync();
        }

        private async void AddContent(object sender, DoWorkEventArgs args)
        {
            KitsuData[] trendingResults;
            trendingResults = await KitsuApi.GetTrendingAnimeAsync();
            try
            {

            }
            catch
            {
                AdonisMessageBox.Show("An error had occurred while getting trending content.", "Otakulore");
                return;
            }
            await Dispatcher.BeginInvoke(() => TrendingList.Items.Clear());
            foreach (var data in trendingResults)
            {
                await Dispatcher.BeginInvoke(() => TrendingList.Items.Add(new ShelfItemModel
                {
                    ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                    Title = data.Attributes.CanonicalTitle,
                    Data = data
                }));
            }
        }

        private void ShowDetails(object sender, MouseButtonEventArgs args)
        {
            if (TrendingList.SelectedItem is not ShelfItemModel model)
                return;
            App.NavigateSinglePage(new AnimeDetailsView(model.Data));
        }

    }

}
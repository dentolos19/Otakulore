using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using Otakulore.Core.Kitsu;
using Otakulore.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace Otakulore.Views
{

    public partial class SearchView
    {

        private readonly string _query;
        private readonly BackgroundWorker _worker;

        public SearchView(string query)
        {
            InitializeComponent();
            _query = query;
            _worker = new BackgroundWorker();
            _worker.DoWork += LoadContent;
            _worker.RunWorkerAsync();
        }

        private async void LoadContent(object sender, DoWorkEventArgs args)
        {
            KitsuData[] searchResults;
            try
            {
                searchResults = await KitsuApi.SearchAnimeAsync(_query);
            }
            catch
            {
                AdonisMessageBox.Show("An error had occurred while searching for content.", "Otakulore");
                return;
            }
            if (!(searchResults.Length > 0))
            {
                AdonisMessageBox.Show("No content has matched your query.", "Otakulore");
                return;
            }
            await Dispatcher.BeginInvoke(() => ContentList.Items.Clear());
            foreach (var data in searchResults)
            {
                await Dispatcher.BeginInvoke(() => ContentList.Items.Add(new ShelfItemModel
                {
                    ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                    Title = data.Attributes.CanonicalTitle,
                    Data = data
                }));
            }
        }

        private void ShowDetails(object sender, MouseButtonEventArgs args)
        {
            if (ContentList.SelectedItem is not ShelfItemModel model)
                return;
            App.NavigateSinglePage(new DetailsView(model.Data));
        }

    }

}
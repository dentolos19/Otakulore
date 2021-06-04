using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using Otakulore.Core.Anime;
using Otakulore.Core.Kitsu;
using Otakulore.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace Otakulore.Views
{

    public partial class SearchView
    {

        private readonly string _query;
        private readonly BackgroundWorker _worker;

        private int _currentPage = 1;

        public SearchView(string query)
        {
            InitializeComponent();
            _query = query;
            _worker = new BackgroundWorker();
            _worker.DoWork += LoadContent;
            _worker.RunWorkerAsync();
        }

        private async void LoadContent(object? sender, DoWorkEventArgs args)
        {
            _currentPage = 1;
            KitsuData[] searchResults;
            try
            {
                searchResults = await KitsuApi.SearchAnimeAsync(_query, _currentPage);
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
            await Dispatcher.BeginInvoke(() =>
            {
                foreach (var data in searchResults)
                {
                    ContentList.Items.Add(new ShelfItemModel
                    {
                        ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                        Title = data.Attributes.CanonicalTitle,
                        Data = data
                    });
                }
            });
        }

        private void ShowDetails(object sender, MouseButtonEventArgs args)
        {
            if (ContentList.SelectedItem is not ShelfItemModel model)
                return;
            App.NavigateSinglePage(new DetailsView(model.Data));
        }

        private async void GoPreviousPage(object sender, ExecutedRoutedEventArgs args)
        {
            _currentPage--;
            KitsuData[] searchResults;
            try
            {
                searchResults = await KitsuApi.SearchAnimeAsync(_query, _currentPage);
            }
            catch
            {
                AdonisMessageBox.Show("An error had occurred while turning a page.", "Otakulore");
                _currentPage++;
                return;
            }
            if (!(searchResults.Length > 0))
            {
                AdonisMessageBox.Show("No more content on the previous page.", "Otakulore");
                _currentPage++;
                return;
            }
            await Dispatcher.BeginInvoke(() =>
            {
                ContentList.Items.Clear();
                foreach (var data in searchResults)
                {
                    ContentList.Items.Add(new ShelfItemModel
                    {
                        ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                        Title = data.Attributes.CanonicalTitle,
                        Data = data
                    });
                }
                PageNumberText.Text = _currentPage.ToString();
            });
        }

        private async void GoNextPage(object sender, ExecutedRoutedEventArgs args)
        {
            _currentPage++;
            KitsuData[] searchResults;
            try
            {
                searchResults = await KitsuApi.SearchAnimeAsync(_query, _currentPage);
            }
            catch
            {
                AdonisMessageBox.Show("An error had occurred while turning a page.", "Otakulore");
                _currentPage++;
                return;
            }
            if (!(searchResults.Length > 0))
            {
                AdonisMessageBox.Show("No more content on the next page.", "Otakulore");
                _currentPage++;
                return;
            }
            await Dispatcher.BeginInvoke(() =>
            {
                ContentList.Items.Clear();
                foreach (var data in searchResults)
                {
                    ContentList.Items.Add(new ShelfItemModel
                    {
                        ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                        Title = data.Attributes.CanonicalTitle,
                        Data = data
                    });
                }
                PageNumberText.Text = _currentPage.ToString();
            });
        }

        private void CanGoPreviousPage(object sender, CanExecuteRoutedEventArgs args)
        {
            if (!IsInitialized)
                return;
            args.CanExecute = _currentPage > 1;
        }

    }

}
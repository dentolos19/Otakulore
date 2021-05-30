using System.Windows;
using System.Windows.Input;
using Otakulore.Core.Kitsu;
using Otakulore.Models;

namespace Otakulore.Graphics
{

    public partial class SearchView
    {
        
        public SearchView()
        {
            InitializeComponent();
        }

        private async void SearchForContent(object sender, RoutedEventArgs args)
        {
            var inputQuery = SearchInput.Text;
            if (string.IsNullOrEmpty(inputQuery))
                return;
            KitsuData<KitsuAnimeAttributes>[] searchResults;
            try
            {
                searchResults = await KitsuApi.SearchAnimeAsync(inputQuery);
            }
            catch
            {
                MessageBox.Show("An error had occurred while searching for content.", "Otakulore");
                return;
            }
            if (searchResults?.Length <= 0)
            {
                MessageBox.Show("No content has matched your query.", "Otakulore");
                return;
            }
            SearchOutput.Items.Clear();
            foreach (var data in searchResults)
            {
                SearchOutput.Items.Add(new ShelfItemModel
                {
                    ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                    Title = data.Attributes.CanonicalTitle,
                    Data = data.Attributes
                });
            }
        }

        private void OpenFavorites(object sender, RoutedEventArgs args)
        {
            App.NavigateSinglePage(App.FavoritesViewPage);
        }

        private void OpenSettings(object sender, RoutedEventArgs args)
        {
            App.NavigateSinglePage(App.SettingsViewPage);
        }

        private void ShowDetails(object sender, MouseButtonEventArgs args)
        {
            if (SearchOutput.SelectedItem is not ShelfItemModel model)
                return;
            App.DetailsViewPage.ShowDetails(model.Data);
            App.NavigateSinglePage(App.DetailsViewPage);
        }

    }

}
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

        private async void Search(object sender, RoutedEventArgs args)
        {
            var inputQuery = SearchInput.Text;
            if (string.IsNullOrEmpty(inputQuery))
                return;
            KitsuData<KitsuAnimeAttributes>[] searchResults;
            try
            {
                searchResults = await KitsuApi.FilterAnimeAsync(inputQuery);
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
            foreach (var item in searchResults)
            {
                SearchOutput.Items.Add(new SearchItemModel
                {
                    Image = item.Attributes.PosterImage.OriginalImageUrl,
                    Title = item.Attributes.CanonicalTitle,
                    Data = item.Attributes
                });
            }
        }

        private void OpenSettings(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void OpenDetails(object sender, MouseButtonEventArgs args)
        {
            if (SearchOutput.SelectedItem is not SearchItemModel model)
                return;
            App.DetailsViewPage.ShowDetails(model.Data);
            App.NavigateSinglePageApp(App.DetailsViewPage);
        }

    }

}
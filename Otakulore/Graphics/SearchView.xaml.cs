using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Kitsu.Anime;
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
            List<AnimeDataModel>? searchResults;
            try
            {
                searchResults = (await Anime.GetAnimeAsync(inputQuery)).Data;
            }
            catch
            {
                MessageBox.Show("An error had occurred while searching for content.", "Otakulore");
                return;
            }
            if (searchResults is not { Count: > 0 })
            {
                MessageBox.Show("No content was found matching your query.", "Otakulore");
                return;
            }
            SearchOutput.Items.Clear();
            foreach (var item in searchResults)
            {
                SearchOutput.Items.Add(new SearchItemModel
                {
                    Image = item.Attributes.PosterImage.Original,
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
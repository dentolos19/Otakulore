using System.Windows;
using System.Windows.Input;
using Otakulore.Api.Kitsu;
using Otakulore.Models;

namespace Otakulore.Graphics
{

    public partial class HomeView
    {
        
        public HomeView()
        {
            InitializeComponent();
        }

        private async void Search(object sender, RoutedEventArgs args)
        {
            var inputQuery = SearchInput.Text;
            if (string.IsNullOrEmpty(inputQuery))
                return;
            var searchResults = await KitsuApi.FilterAnime(inputQuery);
            SearchOutput.Items.Clear();
            foreach (var item in searchResults)
            {
                SearchOutput.Items.Add(new SearchItemModel
                {
                    Image = item.Attributes.PosterImage.OriginalImageUrl,
                    Title = item.Attributes.CanonicalTitle,
                    Data = item
                });
            }
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
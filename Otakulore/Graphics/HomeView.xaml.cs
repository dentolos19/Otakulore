using System.Windows;
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
            var response = await KitsuApi.FilterAnime(inputQuery);
            SearchOutput.Items.Clear();
            foreach (var item in response.Data)
            {
                SearchOutput.Items.Add(new SearchItemModel
                {
                    Image = item.Attributes.PosterImage.OriginalImageUrl,
                    Title = item.Attributes.CanonicalTitle
                });
            }
        }

    }

}
using Otakulore.Models;

namespace Otakulore.Views;

public partial class SearchPage
{

    public SearchPage()
    {
        InitializeComponent();
    }

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is SearchItemModel item)
            await Shell.Current.GoToAsync("details?id=" + item.Id);
    }

}
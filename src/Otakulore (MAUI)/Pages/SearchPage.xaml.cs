using Otakulore.Models;

namespace Otakulore.Pages;

public partial class SearchPage
{

    public SearchPage()
    {
        InitializeComponent();
        BindingContext = new SearchViewModel();
    }

}
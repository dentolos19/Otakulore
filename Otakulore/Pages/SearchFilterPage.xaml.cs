using Otakulore.Models;

namespace Otakulore.Pages;

public partial class SearchFilterPage
{

    public SearchFilterPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SearchFilterViewModel>();
    }

}
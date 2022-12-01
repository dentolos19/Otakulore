using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageRoute]
public partial class SearchFilterPage
{

    public SearchFilterPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SearchFilterPageModel>();
    }

}
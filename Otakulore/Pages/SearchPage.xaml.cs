using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageRoute]
public partial class SearchPage
{

    public SearchPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SearchPageModel>();
    }

}
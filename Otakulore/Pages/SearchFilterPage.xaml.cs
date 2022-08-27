using Otakulore.Core.Attributes;
using Otakulore.Models;

namespace Otakulore.Pages;

[IncludePageRoute]
public partial class SearchFilterPage
{

    public SearchFilterPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SearchFilterPageModel>();
    }

}
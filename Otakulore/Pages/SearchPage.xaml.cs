using Otakulore.Core.Attributes;
using Otakulore.Models;

namespace Otakulore.Pages;

[IncludePageRoute]
public partial class SearchPage
{

    public SearchPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SearchPageModel>();
    }

}
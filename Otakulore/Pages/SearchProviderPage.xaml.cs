using Otakulore.Core.Attributes;
using Otakulore.Models;

namespace Otakulore.Pages;

[IncludePageRoute]
public partial class SearchProviderPage
{

    public SearchProviderPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SearchProviderPageModel>();
    }

}
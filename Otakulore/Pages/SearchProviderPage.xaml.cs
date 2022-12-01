using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageRoute]
public partial class SearchProviderPage
{

    public SearchProviderPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SearchProviderPageModel>();
    }

}
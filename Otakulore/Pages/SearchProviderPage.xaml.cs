using Otakulore.Models;

namespace Otakulore.Pages;

public partial class SearchProviderPage
{

    public SearchProviderPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SearchProviderViewModel>();
    }

}
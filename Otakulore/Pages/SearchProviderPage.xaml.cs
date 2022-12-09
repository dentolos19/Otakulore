using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(SearchProviderPageModel))]
public partial class SearchProviderPage
{

    public SearchProviderPage()
    {
        InitializeComponent();
    }

}
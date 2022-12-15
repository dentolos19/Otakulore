using Otakulore.Models;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(SearchProviderPageModel))]
public partial class SearchProviderPage
{

    public SearchProviderPage()
    {
        InitializeComponent();
    }

}
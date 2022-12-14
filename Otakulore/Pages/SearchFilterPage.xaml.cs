using Otakulore.Helpers;
using Otakulore.Models;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(SearchFilterPageModel))]
public partial class SearchFilterPage
{

    public SearchFilterPage()
    {
        InitializeComponent();
    }

}
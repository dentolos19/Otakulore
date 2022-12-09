using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(SearchFilterPageModel))]
public partial class SearchFilterPage
{

    public SearchFilterPage()
    {
        InitializeComponent();
    }

}
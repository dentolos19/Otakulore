using Otakulore.Helpers;
using Otakulore.Models;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(SearchPageModel))]
public partial class SearchPage
{

    public SearchPage()
    {
        InitializeComponent();
    }

}
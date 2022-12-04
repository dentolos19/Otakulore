using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(SearchPageModel))]
public partial class SearchPage
{

    public SearchPage()
    {
        InitializeComponent();
    }

}
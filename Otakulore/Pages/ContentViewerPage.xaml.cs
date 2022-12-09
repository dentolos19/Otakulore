using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(ContentViewerPageModel))]
public partial class ContentViewerPage
{

    public ContentViewerPage()
    {
        InitializeComponent();
    }

}
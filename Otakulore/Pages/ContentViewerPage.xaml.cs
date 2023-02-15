using Otakulore.Models;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(ContentViewerPageModel))]
public partial class ContentViewerPage
{
    public ContentViewerPage()
    {
        InitializeComponent();
    }
}
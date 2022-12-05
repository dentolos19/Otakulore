using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(SourceViewerPageModel))]
public partial class SourceViewerPage
{

    public SourceViewerPage()
    {
        InitializeComponent();
    }

}
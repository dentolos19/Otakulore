using Otakulore.Models;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(SourceViewerPageModel))]
public partial class SourceViewerPage
{
    public SourceViewerPage()
    {
        InitializeComponent();
    }
}
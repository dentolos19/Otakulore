using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageRoute]
public partial class SourceViewerPage
{

    public SourceViewerPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SourceViewerPageModel>();
    }

}
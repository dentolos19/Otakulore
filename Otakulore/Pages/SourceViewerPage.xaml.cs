using Otakulore.Core.Attributes;
using Otakulore.Models;

namespace Otakulore.Pages;

[IncludePageRoute]
public partial class SourceViewerPage
{

    public SourceViewerPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SourceViewerPageModel>();
    }

}
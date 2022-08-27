using Otakulore.Core.Attributes;
using Otakulore.Models;

namespace Otakulore.Pages;

[IncludePageRoute]
public partial class ContentViewerPage
{

    public ContentViewerPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<ContentViewerPageModel>();
    }

}
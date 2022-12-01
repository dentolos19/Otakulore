using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageRoute]
public partial class ContentViewerPage
{

    public ContentViewerPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<ContentViewerPageModel>();
    }

}
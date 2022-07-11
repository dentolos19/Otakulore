using Otakulore.Models;

namespace Otakulore.Pages;

public partial class ContentViewerPage
{

    public ContentViewerPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<ContentViewerViewModel>();
    }

}
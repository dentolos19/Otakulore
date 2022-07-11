using Otakulore.Models;

namespace Otakulore.Pages;

public partial class SourceViewerPage
{

    public SourceViewerPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SourceViewerViewModel>();
    }

}
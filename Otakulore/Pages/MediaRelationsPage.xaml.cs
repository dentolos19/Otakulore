using Otakulore.Models;

namespace Otakulore.Pages;

public partial class MediaRelationsPage
{

    public MediaRelationsPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<MediaRelationsViewModel>();
    }

}
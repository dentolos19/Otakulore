using Otakulore.Models;

namespace Otakulore.Pages;

public partial class MediaTrackPage
{

    public MediaTrackPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<MediaTrackViewModel>();
    }

}
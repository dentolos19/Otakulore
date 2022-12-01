using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageRoute]
public partial class MediaTrackPage
{

    public MediaTrackPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<MediaTrackPageModel>();
    }

}
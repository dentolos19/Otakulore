using Otakulore.Models;

namespace Otakulore.Pages;

public partial class TrackPage
{

    public TrackPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<TrackViewModel>();
    }

}
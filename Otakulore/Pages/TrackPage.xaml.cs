using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(TrackPageModel))]
public partial class TrackPage
{

    public TrackPage()
    {
        InitializeComponent();
    }

}
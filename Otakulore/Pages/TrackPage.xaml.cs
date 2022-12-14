using Otakulore.Helpers;
using Otakulore.Models;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(TrackPageModel))]
public partial class TrackPage
{

    public TrackPage()
    {
        InitializeComponent();
    }

}
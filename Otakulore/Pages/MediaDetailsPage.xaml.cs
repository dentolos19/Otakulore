using Otakulore.Helpers;
using Otakulore.Models;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(MediaDetailsPageModel))]
public partial class MediaDetailsPage
{

    public MediaDetailsPage()
    {
        InitializeComponent();
    }

}
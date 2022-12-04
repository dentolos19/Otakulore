using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(MediaDetailsPageModel))]
public partial class MediaDetailsPage
{

    public MediaDetailsPage()
    {
        InitializeComponent();
    }

}
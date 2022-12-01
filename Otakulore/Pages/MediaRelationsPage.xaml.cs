using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageRoute]
public partial class MediaRelationsPage
{

    public MediaRelationsPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<MediaRelationsPageModel>();
    }

}
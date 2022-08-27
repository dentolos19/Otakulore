using Otakulore.Core.Attributes;
using Otakulore.Models;

namespace Otakulore.Pages;

[IncludePageRoute]
public partial class MediaRelationsPage
{

    public MediaRelationsPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<MediaRelationsPageModel>();
    }

}
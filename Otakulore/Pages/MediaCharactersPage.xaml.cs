using Otakulore.Core.Attributes;
using Otakulore.Models;

namespace Otakulore.Pages;

[IncludePageRoute]
public partial class MediaCharactersPage
{

    public MediaCharactersPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<MediaCharactersPageModel>();
    }

}
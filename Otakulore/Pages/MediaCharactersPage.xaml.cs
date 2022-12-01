using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageRoute]
public partial class MediaCharactersPage
{

    public MediaCharactersPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<MediaCharactersPageModel>();
    }

}
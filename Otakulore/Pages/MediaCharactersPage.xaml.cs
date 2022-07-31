using Otakulore.Models;

namespace Otakulore.Pages;

public partial class MediaCharactersPage
{

    public MediaCharactersPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<MediaCharactersViewModel>();
    }

}
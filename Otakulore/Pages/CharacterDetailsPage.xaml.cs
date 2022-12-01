using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageRoute]
public partial class CharacterDetailsPage
{

    public CharacterDetailsPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<CharacterDetailsPageModel>();
    }

}
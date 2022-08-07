using Otakulore.Models;

namespace Otakulore.Pages;

public partial class CharacterDetailsPage
{

    public CharacterDetailsPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<CharacterDetailsViewModel>();
    }

}
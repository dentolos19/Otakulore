using Otakulore.Core.Attributes;
using Otakulore.Models;

namespace Otakulore.Pages;

[IncludePageRoute]
public partial class CharacterDetailsPage
{

    public CharacterDetailsPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<CharacterDetailsPageModel>();
    }

}
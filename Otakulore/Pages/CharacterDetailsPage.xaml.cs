using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[TransientService]
[AttachModel(typeof(CharacterDetailsPageModel))]
public partial class CharacterDetailsPage
{

    public CharacterDetailsPage()
    {
        InitializeComponent();
    }

}
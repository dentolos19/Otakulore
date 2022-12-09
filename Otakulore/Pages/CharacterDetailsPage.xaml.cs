using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient, typeof(CharacterDetailsPageModel))]
public partial class CharacterDetailsPage
{

    public CharacterDetailsPage()
    {
        InitializeComponent();
    }

}
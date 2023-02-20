using Otakulore.Models;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Singleton, typeof(ProfilePageModel))]
public partial class ProfilePage
{
    public ProfilePage()
    {
        InitializeComponent();
    }
}
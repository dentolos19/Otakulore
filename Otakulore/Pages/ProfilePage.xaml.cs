using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Singleton, typeof(ProfilePageModel))]
public partial class ProfilePage
{

    public ProfilePage()
    {
        InitializeComponent();
    }

}
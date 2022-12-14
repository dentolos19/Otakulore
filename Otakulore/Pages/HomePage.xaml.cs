using Otakulore.Helpers;
using Otakulore.Models;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Singleton, typeof(HomePageModel))]
public partial class HomePage
{

    public HomePage()
    {
        InitializeComponent();
    }

}
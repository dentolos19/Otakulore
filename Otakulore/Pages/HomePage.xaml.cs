using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Singleton, typeof(HomePageModel))]
public partial class HomePage
{

    public HomePage()
    {
        InitializeComponent();
    }

}
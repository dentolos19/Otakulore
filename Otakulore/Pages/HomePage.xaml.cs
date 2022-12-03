using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[SingletonService]
[AttachModel(typeof(HomePageModel))]
public partial class HomePage
{

    public HomePage()
    {
        InitializeComponent();
    }

}
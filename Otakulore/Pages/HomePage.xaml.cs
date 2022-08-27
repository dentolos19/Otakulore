using Otakulore.Models;

namespace Otakulore.Pages;

public partial class HomePage
{

    public HomePage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<HomePageModel>();
    }

}
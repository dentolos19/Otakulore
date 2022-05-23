namespace Otakulore.Pages;

public partial class HomePage
{

    public HomePage()
    {
        InitializeComponent();
        Shell.Current.Handler.UpdateValue("SearchHandler");
    }

}
using Otakulore.Models;

namespace Otakulore.Pages;

public partial class DetailsPage
{

    public DetailsPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<DetailsViewModel>();
    }

}
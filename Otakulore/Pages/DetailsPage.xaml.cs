using Otakulore.Core.Attributes;
using Otakulore.Models;

namespace Otakulore.Pages;

[IncludePageRoute]
public partial class DetailsPage
{

    public DetailsPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<DetailsPageModel>();
    }

}
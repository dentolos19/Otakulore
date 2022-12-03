using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[TransientService]
[AttachModel(typeof(SearchPageModel))]
public partial class SearchPage
{

    public SearchPage()
    {
        InitializeComponent();
    }

}
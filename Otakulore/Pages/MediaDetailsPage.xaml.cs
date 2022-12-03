using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[TransientService]
[AttachModel(typeof(MediaDetailsPageModel))]
public partial class MediaDetailsPage
{

    public MediaDetailsPage()
    {
        InitializeComponent();
    }

}
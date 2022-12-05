using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Singleton, typeof(SettingsPageModel))]
public partial class SettingsPage
{

    public SettingsPage()
    {
        InitializeComponent();
    }

}
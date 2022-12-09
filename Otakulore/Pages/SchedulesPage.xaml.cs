using Otakulore.Helpers;
using Otakulore.Models;

namespace Otakulore.Pages;

[PageService(PageServiceType.Singleton, typeof(SchedulesPageModel))]
public partial class SchedulesPage
{

    public SchedulesPage()
    {
        InitializeComponent();
    }

}
using Otakulore.Models;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Singleton, typeof(SchedulesPageModel))]
public partial class SchedulesPage
{
    public SchedulesPage()
    {
        InitializeComponent();
    }
}
using Otakulore.Models;
using Syncfusion.Maui.Scheduler;

namespace Otakulore.Pages;

public partial class SchedulePage
{

    public SchedulePage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SchedulePageModel>();
    }

    private async void OnTapped(object? sender, SchedulerTappedEventArgs args)
    {
        if (args.Appointments is not { Count: > 0 })
            return;
        await MauiHelper.Navigate(
            typeof(DetailsPage),
            new Dictionary<string, object>
            {
                { "id", ((MediaScheduleItemModel)args.Appointments.First()).Id }
            }
        );
    }

}
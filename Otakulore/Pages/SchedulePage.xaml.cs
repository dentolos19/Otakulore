using Otakulore.Models;
using Syncfusion.Maui.Scheduler;

namespace Otakulore.Pages;

public partial class SchedulePage
{

    public SchedulePage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<ScheduleViewModel>();
    }

    private async void OnTapped(object? sender, SchedulerTappedEventArgs args)
    {
        if (args.Appointments is not { Count: > 0 })
            return;
        await MauiHelper.NavigateTo(
            typeof(DetailsPage),
            new Dictionary<string, object>
            {
                { "id", ((MediaScheduleItemModel)args.Appointments.First()).Id }
            }
        );
    }

}
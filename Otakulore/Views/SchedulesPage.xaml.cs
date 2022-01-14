using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Otakulore.Models;

namespace Otakulore.Views;

public partial class SchedulesPage
{

    public SchedulesPage()
    {
        InitializeComponent();
        Task.Run(async () =>
        {
            var schedule = await App.Jikan.GetSchedule();
            Dispatcher.Invoke(() =>
            {
                foreach (var entry in schedule.Monday)
                    MondayColumn.Items.Add(MediaItemModel.Create(entry));
                foreach (var entry in schedule.Tuesday)
                    TuesdayColumn.Items.Add(MediaItemModel.Create(entry));
                foreach (var entry in schedule.Wednesday)
                    WednesdayColumn.Items.Add(MediaItemModel.Create(entry));
                foreach (var entry in schedule.Thursday)
                    ThursdayColumn.Items.Add(MediaItemModel.Create(entry));
                foreach (var entry in schedule.Friday)
                    FridayColumn.Items.Add(MediaItemModel.Create(entry));
                foreach (var entry in schedule.Saturday)
                    SaturdayColumn.Items.Add(MediaItemModel.Create(entry));
                foreach (var entry in schedule.Sunday)
                    SundayColumn.Items.Add(MediaItemModel.Create(entry));
            });
        });
    }

    private void OpenMedia(object sender, MouseButtonEventArgs args)
    {
        if (sender is ListBox { SelectedItem: MediaItemModel item })
            NavigationService.Navigate(new DetailsPage(item.Type, item.Id));
    }

}
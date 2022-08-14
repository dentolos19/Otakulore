using Otakulore.Models;

namespace Otakulore.Pages;

public partial class SchedulePage
{

    public SchedulePage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<ScheduleViewModel>();
    }

}
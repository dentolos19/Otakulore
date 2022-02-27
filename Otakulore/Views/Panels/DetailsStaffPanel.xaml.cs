using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core.AniList;
using Otakulore.Models;

namespace Otakulore.Views.Panels;

public sealed partial class DetailsStaffPanel
{

    public DetailsStaffPanel()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not MediaExtra media)
            return;
        foreach (var staff in media.Staff)
            StaffList.Items.Add(new MediaItemModel(staff));
    }

}
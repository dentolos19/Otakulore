using Microsoft.UI.Xaml;

namespace Otakulore.Views.Dialogs;

public sealed partial class ManageTrackerDialog
{

    public ManageTrackerDialog()
    {
        XamlRoot = App.MainWindow.Content.XamlRoot;
        InitializeComponent();
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

}
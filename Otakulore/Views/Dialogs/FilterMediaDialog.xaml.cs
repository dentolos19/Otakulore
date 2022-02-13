using Microsoft.UI.Xaml;

namespace Otakulore.Views.Dialogs;

public sealed partial class FilterMediaDialog
{

    public FilterMediaDialog()
    {
        XamlRoot = App.Window.Content.XamlRoot;
        InitializeComponent();
    }

    private void OnFilter(object sender, RoutedEventArgs args)
    {
        Hide();
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

}
using Microsoft.UI.Xaml;

namespace Otakulore.Views.Dialogs;

public sealed partial class FilterMediaDialog
{

    public FilterMediaDialog()
    {
        InitializeComponent();
        foreach (var genre in App.Genres)
            GenreList.Items.Add(genre);
        foreach (var tag in App.Tags)
            TagList.Items.Add(tag.Name);
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
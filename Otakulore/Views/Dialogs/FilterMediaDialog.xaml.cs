using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core.AniList;
using Otakulore.Core.Helpers;

namespace Otakulore.Views.Dialogs;

public sealed partial class FilterMediaDialog
{

    public FilterMediaDialog()
    {
        InitializeComponent();
        TypeDropdown.Items.Add(new ComboBoxItem { Content = "All" });
        foreach (var status in (MediaType[])Enum.GetValues(typeof(MediaType)))
            TypeDropdown.Items.Add(new ComboBoxItem { Content = status.ToEnumDescription(true), Tag = status });
        foreach (var genre in App.Genres)
            GenreDropdown.Items.Add(new ComboBoxItem { Content = genre });
        foreach (var tag in App.Tags)
            TagDropdown.Items.Add(new ComboBoxItem { Content = tag.Name, Tag = tag });
        TypeDropdown.SelectedIndex = 0;
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
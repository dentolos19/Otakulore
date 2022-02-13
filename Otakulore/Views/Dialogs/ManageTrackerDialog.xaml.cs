using System;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;

namespace Otakulore.Views.Dialogs;

public sealed partial class ManageTrackerDialog
{

    public MediaEntry? Result { get; }

    public ManageTrackerDialog(MediaEntry? entry)
    {
        Result = entry;
        InitializeComponent();
        foreach (var status in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            StatusDropdown.Items.Add(new ComboBoxItem { Content = status.GetEnumDescription(true), Tag = status });
        StatusDropdown.SelectedIndex = 1;
        if (entry == null)
            return;
        StatusDropdown.SelectedItem = StatusDropdown.Items.FirstOrDefault(item => (MediaEntryStatus)((ComboBoxItem)item).Tag == entry.Status);
        ProgressBox.Value = entry.Progress;
    }

    private void OnSave(object sender, RoutedEventArgs args)
    {
        Hide();
    }

    private void OnDelete(object sender, RoutedEventArgs args)
    {
        Hide();
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

}
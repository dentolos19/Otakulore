using System;
using System.Linq;
using AniListNet.Models;
using AniListNet.Objects;
using Humanizer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Otakulore.Views.Dialogs;

public sealed partial class ManageTrackerDialog
{

    private readonly int _id;

    public MediaEntry? Result { get; private set; }

    public ManageTrackerDialog(MediaEntry entry)
    {
        Result = entry;
        _id = Result.Media.Id;
        InitializeComponent();
        foreach (var status in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            StatusDropdown.Items.Add(new ComboBoxItem { Content = status.Humanize(), Tag = status });
        StatusDropdown.SelectedItem = StatusDropdown.Items.FirstOrDefault(item => (MediaEntryStatus)((ComboBoxItem)item).Tag == entry.Status);
        ProgressBox.Value = entry.Progress;
    }

    public ManageTrackerDialog(int id)
    {
        _id = id;
        InitializeComponent();
        foreach (var status in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            StatusDropdown.Items.Add(new ComboBoxItem { Content = status.Humanize(), Tag = status });
        StatusDropdown.SelectedIndex = 1;
        DeleteButton.IsEnabled = false;
    }

    private async void OnSave(object sender, RoutedEventArgs args)
    {
        if (StatusDropdown.SelectedItem is not ComboBoxItem { Tag: MediaEntryStatus status })
            return;
        Result = await App.Client.SaveMediaEntryAsync(_id, new MediaEntryMutation
        {
            Status = status,
            Progress = (int)ProgressBox.Value
        });
        Hide();
    }

    private async void OnDelete(object sender, RoutedEventArgs args)
    {
        if (Result == null)
            return;
        var result = await App.Client.DeleteMediaEntryAsync(Result.Id);
        if (result)
            Result = null;
        else
            App.AttachNotification("Unable to delete entry!");
        Hide();
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

}
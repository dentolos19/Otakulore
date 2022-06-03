using System;
using System.Linq;
using AniListNet.Models;
using AniListNet.Objects;
using Humanizer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Services;

namespace Otakulore.Views.Dialogs;

public sealed partial class FilterMediaDialog
{

    public SearchMediaFilter? Result { get; private set; }

    public FilterMediaDialog(SearchMediaFilter? filter = null)
    {
        InitializeComponent();
        TypeDropdown.Items.Add(new ComboBoxItem { Content = "All" });
        foreach (var status in (MediaType[])Enum.GetValues(typeof(MediaType)))
            TypeDropdown.Items.Add(new ComboBoxItem { Content = status.Humanize(), Tag = status });
        foreach (var genre in App.Genres)
            GenreDropdown.Items.Add(new ComboBoxItem { Content = genre });
        TypeDropdown.SelectedIndex = 0;
        GenreDropdown.SelectedIndex = ServiceUtilities.Random.Next(GenreDropdown.Items.Count);
        if (filter == null)
            return;
        TypeDropdown.SelectedItem = TypeDropdown.Items.OfType<ComboBoxItem>().FirstOrDefault(item => (MediaType?)item.Tag == filter.Type) ?? TypeDropdown.Items.First();
        foreach (var genre in filter.Genres)
            GenreList.Items.Add(new FilterItemModel(genre, genre));
    }

    private void OnAddGenreFilter(object sender, RoutedEventArgs args)
    {
        if (GenreDropdown.SelectedItem is ComboBoxItem { Content: string genre })
            GenreList.Items.Add(new FilterItemModel(genre, genre));
    }

    private void OnRemoveGenreItem(object sender, RoutedEventArgs args)
    {
        GenreList.Items.Remove(((FrameworkElement)sender).DataContext);
    }

    private void OnFilter(object sender, RoutedEventArgs args)
    {
        Result = new SearchMediaFilter();
        if (TypeDropdown.SelectedItem is ComboBoxItem { Tag: MediaType type })
            Result.Type = type;
        var genres = GenreList.Items.OfType<FilterItemModel>().ToArray().Select(item => item.Data).OfType<string>().ToArray();
        if (genres.Length > 0)
            foreach (var item in genres)
                Result.Genres.Add(item);
        Hide();
    }

    private void OnReset(object sender, RoutedEventArgs args)
    {
        Result = new SearchMediaFilter();
        Hide();
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

}
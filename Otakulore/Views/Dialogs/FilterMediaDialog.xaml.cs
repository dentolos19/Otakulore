using System;
using System.Linq;
using Humanizer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;

namespace Otakulore.Views.Dialogs;

public sealed partial class FilterMediaDialog
{

    public AniFilter? Result { get; private set; }

    public FilterMediaDialog(AniFilter? filter = null)
    {
        InitializeComponent();
        TypeDropdown.Items.Add(new ComboBoxItem { Content = "All" });
        foreach (var status in (MediaType[])Enum.GetValues(typeof(MediaType)))
            TypeDropdown.Items.Add(new ComboBoxItem { Content = status.Humanize(), Tag = status });
        foreach (var genre in App.Genres)
            GenreDropdown.Items.Add(new ComboBoxItem { Content = genre });
        foreach (var tag in App.Tags)
            TagDropdown.Items.Add(new ComboBoxItem { Content = tag.Name });
        TypeDropdown.SelectedIndex = 0;
        GenreDropdown.SelectedIndex = Utilities.Random.Next(GenreDropdown.Items.Count);
        TagDropdown.SelectedIndex = Utilities.Random.Next(TagDropdown.Items.Count);
        if (filter == null)
            return;
        TypeDropdown.SelectedItem = TypeDropdown.Items.OfType<ComboBoxItem>().FirstOrDefault(item => (MediaType?)item.Tag == filter.Type) ?? TypeDropdown.Items.First();
        foreach (var genre in filter.Genres)
            GenreList.Items.Add(new FilterItemModel(Symbol.Add, genre, genre));
        foreach (var tag in filter.Tags)
            GenreList.Items.Add(new FilterItemModel(Symbol.Add, tag, tag));
    }

    private void OnAddGenreFilter(object sender, RoutedEventArgs args)
    {
        if (GenreDropdown.SelectedItem is ComboBoxItem { Content: string genre })
            GenreList.Items.Add(new FilterItemModel(Symbol.Add, genre, genre));
    }

    private void OnAddTagFilter(object sender, RoutedEventArgs args)
    {
        if (TagDropdown.SelectedItem is ComboBoxItem { Content: string tag })
            TagList.Items.Add(new FilterItemModel(Symbol.Add, tag, tag));
    }

    private void OnRemoveGenreItem(object sender, RoutedEventArgs args)
    {
        GenreList.Items.Remove(((FrameworkElement)sender).DataContext);
    }

    private void OnRemoveTagItem(object sender, RoutedEventArgs args)
    {
        TagList.Items.Remove(((FrameworkElement)sender).DataContext);
    }

    private void OnFilter(object sender, RoutedEventArgs args)
    {
        Result = new AniFilter();
        if (TypeDropdown.SelectedItem is ComboBoxItem { Tag: MediaType type })
            Result.Type = type;
        var genres = GenreList.Items.OfType<FilterItemModel>().ToArray().Select(item => item.Data).OfType<string>().ToArray();
        var tags = TagList.Items.OfType<FilterItemModel>().ToArray().Select(item => item.Data).OfType<string>().ToArray();
        if (genres.Length > 0)
            foreach (var item in genres)
                Result.Genres.Add(item);
        if (tags.Length > 0)
            foreach (var item in tags)
                Result.Tags.Add(item);
        Hide();
    }

    private void OnReset(object sender, RoutedEventArgs args)
    {
        Result = new AniFilter();
        Hide();
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

}
using Humanizer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;

namespace Otakulore.Views.Panels;

public sealed partial class DetailsOverviewPanel
{

    public DetailsOverviewPanel()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not MediaExtra media)
            return;
        FormatText.Text = media.Format?.Humanize() ?? "Unknown";
        StatusText.Text = media.Status.Humanize();
        if (media.Format == MediaFormat.Movie)
        {
            ContentTextLabel.Text = "Length";
            ContentText.Text = media.Duration.HasValue ? media.Duration.Value + " minutes" : "Unknown";
        }
        else
        {
            ContentTextLabel.Text = media.Type switch
            {
                MediaType.Anime => "Episodes",
                MediaType.Manga => "Chapters",
                _ => ContentTextLabel.Text
            };
            ContentText.Text = media.Content.HasValue ? media.Content.Value.ToString() : "Unknown";
        }
        StartDateText.Text = media.StartDate?.ToString() ?? "Unknown";
        EndDateText.Text = media.EndDate?.ToString() ?? "Unknown";
        DescriptionText.Text = media.Description != null ? Utilities.ConvertHtmlToMarkdown(media.Description) : "No description provided.";
        if (media.Genres is { Length: > 0 })
            foreach (var genre in media.Genres)
                GenreList.Items.Add(genre);
        else
            GenreSection.Visibility = Visibility.Collapsed;
        if (media.Tags is { Length: > 0 })
            foreach (var tag in media.Tags)
                TagList.Items.Add(tag.Name);
        else
            TagSection.Visibility = Visibility.Collapsed;
    }

}
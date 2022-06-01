using AniListNet;
using AniListNet.Objects;
using Humanizer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;

namespace Otakulore.Views.Panels;

public sealed partial class DetailsOverviewPanel
{

    public DetailsOverviewPanel()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not Media media)
            return;
        FormatText.Text = media.Format.Humanize(LetterCasing.Title);
        StatusText.Text = media.Status.Humanize();
        if (media.Format == MediaFormat.Movie)
        {
            ContentTextLabel.Text = "Length";
            ContentText.Text = media.Duration.HasValue ? media.Duration.Value + " minutes" : "Unknown";
        }
        else
        {
            switch (media.Type)
            {
                case MediaType.Anime:
                    ContentTextLabel.Text = "Episodes";
                    ContentText.Text = media.Episodes.HasValue ? media.Episodes.Value.ToString() : "Unknown";
                    break;
                case MediaType.Manga:
                    ContentTextLabel.Text = "Chapters";
                    ContentText.Text = media.Chapters.HasValue ? media.Chapters.Value.ToString() : "Unknown";
                    break;
            }
        }
        StartDateText.Text = media.StartDate?.ToDateOnly().ToString() ?? "Unknown";
        EndDateText.Text = media.EndDate?.ToDateOnly().ToString() ?? "Unknown";
        DescriptionText.Text = !string.IsNullOrEmpty(media.Description) ? Utilities.ConvertHtmlToMarkdown(media.Description) : "No description provided.";
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
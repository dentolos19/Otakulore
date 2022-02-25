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
        FormatText.Text = media.Format.ToEnumDescription(true);
        StatusText.Text = media.Status.ToEnumDescription(true);
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
        DescriptionText.Text = media.Description != null ? Utilities.ConvertHtmlToPlainText(media.Description) : "No description provided.";
    }

}
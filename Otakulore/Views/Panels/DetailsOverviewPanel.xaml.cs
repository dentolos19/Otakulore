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
        if (args.Parameter is not Media media)
            return;
        FormatText.Text = media.Format.GetEnumDescription(true);
        StatusText.Text = media.Status.GetEnumDescription(true);
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
        StartDateText.Text = media.StartDate.ToString() ?? "Unknown";
        EndDateText.Text = media.EndDate.ToString() ?? "Unknown";
        DescriptionText.Text = media.Description != null ? Utilities.ConvertHtmlToPlainText(media.Description) : "No description provided.";
    }

}
namespace Otakulore.Core;

public class MediaSource
{

    public Uri ImageUrl { get; }
    public string Title { get; }
    public Uri Url { get; }

    public MediaSource(Uri imageUrl, string title, Uri url)
    {
        ImageUrl = imageUrl;
        Title = title;
        Url = url;
    }

}
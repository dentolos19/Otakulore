namespace Otakulore.Core;

public class MediaSource
{

    public Uri ImageUrl { get; }
    public string Title { get; }
    public object Data { get; }

    public MediaSource(Uri imageUrl, string title, object data)
    {
        ImageUrl = imageUrl;
        Title = title;
        Data = data;
    }

}
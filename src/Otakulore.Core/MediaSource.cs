namespace Otakulore.Core;

public class MediaSource
{

    public string ImageUrl { get; }
    public string Name { get; }
    public string Url { get; }

    public MediaSource(string imageUrl, string name, string url)
    {
        ImageUrl = imageUrl;
        Name = name;
        Url = url;
    }

}
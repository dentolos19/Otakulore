namespace Otakulore.Content;

public class MediaContent
{

    public string Name { get; }
    public Uri Url { get; }

    public MediaContent(string name, Uri url)
    {
        Name = name;
        Url = url;
    }

}
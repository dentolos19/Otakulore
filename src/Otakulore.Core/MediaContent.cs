namespace Otakulore.Core;

public class MediaContent
{

    public string Name { get; }
    public string Url { get; }

    public MediaContent(string name, string url)
    {
        Name = name;
        Url = url;
    }

}
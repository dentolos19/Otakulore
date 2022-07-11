namespace Otakulore.Core;

public class MediaContent
{

    public string Name { get; }
    public object Data { get; }

    public MediaContent(string name, object data)
    {
        Name = name;
        Data = data;
    }

}
using Otakulore.Services;

namespace Otakulore.Models;

public class ContentItemModel
{

    public string Name { get; }
    public MediaContent Data { get; }

    public ContentItemModel(MediaContent data)
    {
        Name = data.Name;
        Data = data;
    }

}
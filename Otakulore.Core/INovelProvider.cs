namespace Otakulore.Core;

public interface INovelProvider : IProvider
{

    public string GetText(MediaContent content);

}
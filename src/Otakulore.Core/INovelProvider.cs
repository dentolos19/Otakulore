namespace Otakulore.Core;

public interface INovelProvider : IProvider
{

    public bool TryExtractText(MediaContent content, out string text);

}
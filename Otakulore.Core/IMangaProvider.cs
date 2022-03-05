namespace Otakulore.Core;

public interface IMangaProvider : IProvider
{

    public bool TryExtractImageUrls(MediaContent content, out string[] urls);

}
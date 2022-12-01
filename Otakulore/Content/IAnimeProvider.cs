namespace Otakulore.Content;

public interface IAnimeProvider : IProvider
{

    public Task<bool> TryExtractVideoPlayerUrl(MediaContent content, out Uri url);

}
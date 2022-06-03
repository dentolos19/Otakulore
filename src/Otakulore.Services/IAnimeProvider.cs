namespace Otakulore.Services;

public interface IAnimeProvider : IProvider
{

    public Task<bool> TryExtractVideoPlayer(MediaContent content, out Uri url);

}
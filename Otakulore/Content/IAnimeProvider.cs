using Otakulore.Content.Objects;

namespace Otakulore.Content;

public interface IAnimeProvider : IProvider
{

    public Task<Uri?> ExtractVideoPlayerUrl(MediaContent content) => Task.FromResult<Uri?>(null);

}
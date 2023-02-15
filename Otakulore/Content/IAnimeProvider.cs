using Otakulore.Content.Objects;

namespace Otakulore.Content;

public interface IAnimeProvider : IProvider
{
    public Task<Uri> ExtractAnimePlayerUrl(MediaContent content)
    {
        throw new NotImplementedException();
    }
}
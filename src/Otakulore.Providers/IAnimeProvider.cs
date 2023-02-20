using Otakulore.Providers.Objects;

namespace Otakulore.Providers;

public interface IAnimeProvider : IProvider
{
    public Task<Uri> ExtractAnimePlayerUrl(MediaContent content)
    {
        throw new NotImplementedException();
    }
}
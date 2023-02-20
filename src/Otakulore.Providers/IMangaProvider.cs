using Otakulore.Providers.Objects;

namespace Otakulore.Providers;

public interface IMangaProvider : IProvider
{
    public Task<Uri> ExtractMangaReaderUrl(MediaContent content)
    {
        throw new NotImplementedException();
    }
}
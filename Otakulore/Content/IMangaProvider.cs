using Otakulore.Content.Objects;

namespace Otakulore.Content;

public interface IMangaProvider : IProvider
{

    public Task<Uri> ExtractMangaReaderUrl(MediaContent content) => throw new NotImplementedException();

}
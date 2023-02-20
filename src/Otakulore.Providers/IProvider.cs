using Otakulore.Providers.Objects;

namespace Otakulore.Providers;

public interface IProvider
{
    public string Name { get; }

    public Task<IList<MediaSource>> GetSources(string query);
    public Task<IList<MediaContent>> GetContents(MediaSource source);
}
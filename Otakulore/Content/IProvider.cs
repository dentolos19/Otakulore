using Otakulore.Content.Objects;

namespace Otakulore.Content;

public interface IProvider
{

    public string Name { get; }

    public Task<IList<MediaSource>> GetSources(string query);
    public Task<IList<MediaContent>> GetContents(MediaSource source);

}
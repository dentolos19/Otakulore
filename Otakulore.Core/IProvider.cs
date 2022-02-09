namespace Otakulore.Core;

public interface IProvider
{

    public string ImageUrl { get; }
    public string Name { get; }

    public IList<MediaSource> GetSources(string query);
    public IList<MediaContent> GetContents(MediaSource source);

}
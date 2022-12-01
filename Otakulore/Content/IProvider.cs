namespace Otakulore.Content;

public interface IProvider
{

    public string Name { get; }

    public Task<MediaSource[]?> GetSources(string query);
    public Task<MediaContent[]?> GetContents(MediaSource source);

}
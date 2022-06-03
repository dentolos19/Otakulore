namespace Otakulore.Services;

public interface IProvider
{

    public Uri ImageUrl { get; }
    public string Name { get; }

    public Task<MediaSource[]?> SearchSourcesAsync(string query);
    public Task<MediaContent[]?> GetContentAsync(MediaSource source);

}
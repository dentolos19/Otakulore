namespace Otakulore.Services;

public interface IProvider
{

    public string Name { get; }

    public Task<MediaSource[]?> SearchAsync(string query);

}
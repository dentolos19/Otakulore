namespace Otakulore.Services;

public interface IProvider
{

    string Name { get; }
    string Author { get; }

    void Initialize();

}
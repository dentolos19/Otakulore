using Otakulore.Services;

namespace Otakulore.Models;

public class ProviderItemModel
{

    public string Name { get; init; }
    public string Author { get; init; }
    public IProvider Provider { get; init; }

    public static ProviderItemModel Create(IProvider provider)
    {
        return new ProviderItemModel
        {
            Name = provider.Name,
            Author = provider.Author,
            Provider = provider
        };
    }

}
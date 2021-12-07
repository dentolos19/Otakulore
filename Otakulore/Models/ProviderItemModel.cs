using Otakulore.Services;

namespace Otakulore.Models;

public class ProviderItemModel
{

    public IProvider Provider { get; init; }

    public static ProviderItemModel Create(IProvider provider)
    {
        return new ProviderItemModel { Provider = provider };
    }

}
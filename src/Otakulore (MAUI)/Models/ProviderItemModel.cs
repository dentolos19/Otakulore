using Otakulore.Services;

namespace Otakulore.Models;

public class ProviderItemModel
{

    public string Name { get; }
    public IProvider Provider { get; }

    public ProviderItemModel(IProvider provider)
    {
        Name = provider.Name;
        Provider = provider;
    }

}
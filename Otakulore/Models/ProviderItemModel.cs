using Otakulore.Core;

namespace Otakulore.Models;

public class ProviderItemModel
{

    public IProvider Provider { get; }

    public ProviderItemModel(IProvider provider)
    {
        Provider = provider;
    }

}
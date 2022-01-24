using Otakulore.Core;

namespace Otakulore.Models;

public class ProviderItemModel
{

    public ProviderItemModel(IProvider provider)
    {
        Provider = provider;
    }

    public IProvider Provider { get; }

}
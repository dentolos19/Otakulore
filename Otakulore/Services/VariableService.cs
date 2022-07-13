using Otakulore.Core;
using Otakulore.Core.Providers;

namespace Otakulore.Services;

public class VariableService
{

    public IList<IProvider> Providers { get; } = new List<IProvider>();

    public static VariableService Initialize()
    {
        var service = new VariableService();
        service.Providers.Add(new GogoanimeProvider());
        return service;
    }

}
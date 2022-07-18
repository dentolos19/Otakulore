using Otakulore.Core;
using Otakulore.Core.Providers;

namespace Otakulore.Services;

public class VariableService
{

    public IList<ResourceDictionary> InitialDictionaries { get; } = new List<ResourceDictionary>();
    public IList<IProvider> ContentProviders { get; } = new List<IProvider>();

    public static VariableService Initialize()
    {
        var service = new VariableService();
        service.ContentProviders.Add(new GogoanimeProvider());
        service.ContentProviders.Add(new ManganatoProvider());
        return service;
    }

}
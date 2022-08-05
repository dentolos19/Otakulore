using AniListNet;
using Otakulore.Core;
using Otakulore.Core.Providers;

namespace Otakulore.Services;

public class VariableService
{

    public AniClient Client { get; set; }
    public IList<IProvider> Providers { get; set; } = new List<IProvider>();

    public static VariableService Initialize()
    {
        var service = new VariableService();
        service.Providers.Add(new GogoanimeProvider());
        service.Providers.Add(new ManganatoProvider());
        return service;
    }

}
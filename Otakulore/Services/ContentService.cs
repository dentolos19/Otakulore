using Otakulore.Content;
using Otakulore.Content.Providers;
using Otakulore.Helpers;

namespace Otakulore.Services;

[SingletonService]
public class ContentService
{

    public static ContentService Instance => MauiHelper.GetService<ContentService>()!;

    public IReadOnlyList<IProvider> Providers { get; } = new List<IProvider>
    {
        new GogoanimeProvider(),
        new ManganatoProvider()
    };

}
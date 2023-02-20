using AniListNet;
using Otakulore.Utilities.Attributes;

namespace Otakulore.Services;

[SingletonService]
public class DataService
{
    public static DataService Instance => MauiHelper.GetService<DataService>()!;

    public AniClient Client { get; private set; } = new();

    public void ResetClient()
    {
        Client = new AniClient();
    }
}
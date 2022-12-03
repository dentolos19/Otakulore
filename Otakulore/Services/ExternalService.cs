using AniListNet;
using Otakulore.Helpers;

namespace Otakulore.Services;

[SingletonService]
public class ExternalService
{

    public AniClient AniClient { get; } = new();

}
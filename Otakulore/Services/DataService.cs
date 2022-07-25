using AniListNet;

namespace Otakulore.Services;

public class DataService
{

    public AniClient Client { get; private set; }

    public void ResetService()
    {
        Client = new AniClient();
    }

    public static DataService Initialize()
    {
        return new DataService
        {
            Client = new AniClient()
        };
    }

}
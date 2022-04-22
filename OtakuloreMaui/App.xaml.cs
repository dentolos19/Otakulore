using Otakulore.Core.AniList;

namespace Otakulore;

public partial class App
{

    public static AniClient Client { get; } = new();

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

}
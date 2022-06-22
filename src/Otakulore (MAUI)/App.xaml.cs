using AniListNet;

namespace Otakulore;

public partial class App
{

    public static AniClient Client { get; } = new();
    public static AppSettings Settings { get; } = new();

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

}
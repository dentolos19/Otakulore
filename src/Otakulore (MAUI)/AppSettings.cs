namespace Otakulore;

public class AppSettings
{

    public string? AccessToken
    {
        get => Preferences.Default.Get<string?>(nameof(AccessToken), null);
        set => Preferences.Default.Set(nameof(AccessToken), value);
    }

}
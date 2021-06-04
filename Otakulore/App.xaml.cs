using System.Windows;
using System.Windows.Controls;
using AdonisUI;
using Otakulore.Core;
using Otakulore.Views;

namespace Otakulore
{

    public partial class App
    {

        internal static FavoritesView FavoritesViewPage { get; } = new();
        internal static TrendingView TrendingViewPage { get; } = new();
        internal static SettingsView SettingsViewPage { get; } = new();

        internal static DiscordRichPresence? RichPresence { get; set; }
        internal static UserData UserPreferences { get; set; }
        
        private void SetupApp(object sender, StartupEventArgs args)
        {
            UserPreferences = UserData.LoadData();
            if (UserPreferences.EnableDiscordRichPresence)
            {
                try
                {
                    RichPresence = new DiscordRichPresence();
                    RichPresence.InitializeRpc("850203114560159774");
                }
                catch
                {
                    // do nothing
                }
            }
            ResourceLocator.SetColorScheme(Current.Resources, UserPreferences.EnableDarkMode ? ResourceLocator.DarkColorScheme : ResourceLocator.LightColorScheme);
        }

        private void DisposeInstances(object sender, ExitEventArgs args)
        {
            RichPresence?.Dispose();
            UserPreferences.SaveData();
        }

        public static void NavigateSinglePage(Page view)
        {
            if (Current.MainWindow is MainWindow window)
                window.View.Navigate(view);
        }

    }

}
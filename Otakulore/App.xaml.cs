using System.Windows;
using System.Windows.Controls;
using Otakulore.Core;

namespace Otakulore
{

    public partial class App
    {

        internal static DiscordRichPresence? RichPresence { get; set; }
        internal static UserData UserPreferences { get; set; }
        
        private void SetupApp(object sender, StartupEventArgs args)
        {
            UserPreferences = UserData.LoadData();
            if (UserPreferences.EnableDiscordRpc)
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
        }

        private void DisposeInstance(object sender, ExitEventArgs args)
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
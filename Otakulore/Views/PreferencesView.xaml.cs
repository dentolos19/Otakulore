using System.Windows;
using AdonisUI;
using Otakulore.Core;

namespace Otakulore.Views
{

    public partial class PreferencesView
    {

        public PreferencesView()
        {
            InitializeComponent();
        }

        private void LoadSettings(object sender, RoutedEventArgs args)
        {
            EnableDarkBox.IsChecked = App.UserPreferences.EnableDarkMode;
            EnableDiscordBox.IsChecked = App.UserPreferences.EnableDiscordRichPresence;
        }

        private void SavePreferences(object sender, RoutedEventArgs args)
        {
            App.UserPreferences.EnableDarkMode = EnableDarkBox.IsChecked == true;
            App.UserPreferences.EnableDiscordRichPresence = EnableDiscordBox.IsChecked == true;
            App.UserPreferences.SaveData();
            ResourceLocator.SetColorScheme(Application.Current.Resources, App.UserPreferences.EnableDarkMode ? ResourceLocator.DarkColorScheme : ResourceLocator.LightColorScheme);
            if (App.UserPreferences.EnableDiscordRichPresence)
            {
                if (App.RichPresence == null || App.RichPresence.IsDisposed)
                    App.RichPresence = new DiscordRichPresence();
                App.RichPresence.SetInitialState();
            }
            else
            {
                App.RichPresence?.Dispose();
            }
        }

    }

}
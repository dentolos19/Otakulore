using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Otakulore.Views
{

    public sealed partial class SettingsView
    {

        public SettingsView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            ShowEpisodeInfoSwitch.IsOn = App.Settings.ShowEpisodeInfo;
        }

        private void UpdateSettings(object sender, RoutedEventArgs args)
        {
            App.Settings.ShowEpisodeInfo = ShowEpisodeInfoSwitch.IsOn;
        }

    }

}
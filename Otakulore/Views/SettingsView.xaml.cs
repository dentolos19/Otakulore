namespace Otakulore.Views
{

    public partial class SettingsView
    {

        public SettingsView()
        {
            InitializeComponent();
            DataContext = App.UserPreferences;
        }

    }

}
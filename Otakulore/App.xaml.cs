using Otakulore.Core;
using Otakulore.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Otakulore
{

    public sealed partial class App
    {

        internal static UserData Settings { get; set; }

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Settings = UserData.LoadData();
            WebDriver.EnsureDriverExists();
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
            }
            if (args.PrelaunchActivated)
                return;
            if (rootFrame.Content == null)
                rootFrame.Navigate(typeof(MainView), args.Arguments);
            Window.Current.Activate();
        }
        
        private void OnSuspending(object sender, SuspendingEventArgs args)
        {
            var deferral = args.SuspendingOperation.GetDeferral();
            Settings.SaveData();
            deferral.Complete();
        }

    }

}
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Views;

namespace Otakulore
{
    
    public sealed partial class App
    {

        internal static UserData Settings { get; } = UserData.LoadData();

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }
        
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            WebDriver.EnsureDriverExists();
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: load state from previously suspended application
                }
                Window.Current.Content = rootFrame;
            }
            if (args.PrelaunchActivated)
                return;
            if (rootFrame.Content == null)
                rootFrame.Navigate(typeof(MainView), args.Arguments);
            Window.Current.Activate();
        }
        
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs args)
        {
            throw new Exception("Failed to load Page " + args.SourcePageType.FullName);
        }
        
        private void OnSuspending(object sender, SuspendingEventArgs args)
        {
            var deferral = args.SuspendingOperation.GetDeferral();
            Settings.SaveData();
            deferral.Complete();
        }

    }

}
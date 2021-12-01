using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Otakulore.Views;

namespace Otakulore
{

    public sealed partial class App
    {

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: load previous state
                }
                Window.Current.Content = rootFrame;
            }
            if (args.PrelaunchActivated)
                return;
            if (rootFrame.Content == null)
                rootFrame.Navigate(typeof(MainPage), args.Arguments);
            Window.Current.Activate();
        }

        private static void OnNavigationFailed(object sender, NavigationFailedEventArgs args)
        {
            throw new Exception("Failed to load page " + args.SourcePageType.FullName);
        }

        private static void OnSuspending(object sender, SuspendingEventArgs args)
        {
            var deferral = args.SuspendingOperation.GetDeferral();
            // TODO: save current state
            deferral.Complete();
        }

    }

}
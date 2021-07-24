using Otakulore.Core;
using Otakulore.Core.Helpers;
using Otakulore.Views;
using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
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
            CoreLogger.PostLine("App was started!");
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
            CoreLogger.SaveToFile(Path.Combine(ApplicationData.Current.LocalCacheFolder.Path, $"{DateTime.Now:yyyyMMdd-HHmmss}.log"));
            deferral.Complete();
        }

    }

}
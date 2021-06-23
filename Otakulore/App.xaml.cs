﻿using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core;
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
            var webDriverPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "EdgeWebDriver.exe");
            if (!File.Exists(webDriverPath))
            {
                var stream = CoreUtilities.GetResourceStream("EdgeWebDriver.exe");
                File.WriteAllBytes(webDriverPath, stream.ToByteArray());
                stream.Close();
            }
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
            // TODO: save application state and stop any background activity
            deferral.Complete();
        }

    }

}
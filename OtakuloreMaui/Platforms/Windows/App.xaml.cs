﻿using Microsoft.UI.Xaml;

namespace Otakulore.WinUI
{

    public partial class App : MauiWinUIApplication
    {

        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    }

}
﻿using System;
using System.Linq;
using AniListNet.Objects;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Models;
using Otakulore.Views.Panels;

namespace Otakulore.Views.Pages;

public sealed partial class ProfilePage
{

    private bool _hasAlreadyNavigated;
    private User _user;

    public ProfilePage()
    {
        InitializeComponent();
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Overview", Tag = typeof(ProfileOverviewPanel) });
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "List", Tag = typeof(ProfileListPanel) });
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (!App.Client.IsAuthenticated)
        {
            var model = new NotificationDataModel
            {
                Message = "This feature requires an authenticated AniList account.",
                ContinueText = "Login"
            };
            model.ContinueClicked += (_, _) => App.NavigateFrame(typeof(LoginPage));
            App.ShowNotification(model);
            return;
        }
        if (_hasAlreadyNavigated)
            return;
        _hasAlreadyNavigated = true;
        _user = await App.Client.GetAuthenticatedUserAsync();
        PanelNavigation.SelectedItem = PanelNavigation.MenuItems.First();
        LoadingIndicator.IsLoading = false;
    }

    private void OnNavigatePanel(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
            PanelFrame.Navigate(typeof(ProfileSettingsPanel));
        else if (args.SelectedItem is NavigationViewItem { Tag: Type type })
            PanelFrame.Navigate(type, _user);
    }

}
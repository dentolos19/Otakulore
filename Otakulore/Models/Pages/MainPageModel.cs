﻿using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Helpers;
using Otakulore.Services;
using Otakulore.Utilities.Attributes;

namespace Otakulore.Models;

[SingletonService]
public partial class MainPageModel : BasePageModel
{

    [ObservableProperty] private string _version;
    [ObservableProperty] private int _rateRemaining;

    public MainPageModel()
    {
        DataService.Instance.Client.RateChanged += (_, args) => RateRemaining = args.RateRemaining;
        #if ANDROID
        var buildVersion = VersionTracking.CurrentBuild;
        #if DEBUG
        buildVersion = "Debug";
        #endif
        Version = $"{VersionTracking.CurrentVersion} ({buildVersion})";
        #else
        var version = VersionTracking.CurrentVersion;
        var buildVersion = version.Split('.')[3];
        #if DEBUG
        buildVersion = "Debug";
        #endif
        Version = $"{version.Remove(version.LastIndexOf("."))} ({buildVersion})";
        #endif
    }

}
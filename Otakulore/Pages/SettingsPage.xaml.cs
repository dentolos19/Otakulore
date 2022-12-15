﻿using Otakulore.Models;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Singleton, typeof(SettingsPageModel))]
public partial class SettingsPage
{

    public SettingsPage()
    {
        InitializeComponent();
    }

}
﻿using Otakulore.Providers;
using Otakulore.Providers.Providers;
using Otakulore.Utilities.Attributes;

namespace Otakulore.Services;

[SingletonService]
public class ContentService
{
    public static ContentService Instance => MauiHelper.GetService<ContentService>()!;

    public IReadOnlyList<IProvider> Providers { get; } = new List<IProvider>
    {
        new GogoanimeProvider(),
        new ManganatoProvider()
    };
}
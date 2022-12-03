﻿using System.Reflection;
using Otakulore.Helpers;
using Otakulore.Models;
using Otakulore.Pages;

namespace Otakulore;

public static class MauiHelper
{

    public static MauiAppBuilder SetupFonts(this MauiAppBuilder builder)
    {
        return builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("Poppins.ttf", "Poppins");
        });
    }

    public static MauiAppBuilder SetupServices(this MauiAppBuilder builder)
    {
        var types =
            from type in Assembly.GetExecutingAssembly().GetTypes()
            where type.Namespace?.StartsWith("Otakulore", StringComparison.OrdinalIgnoreCase) ?? false
            select type;
        foreach (var type in types)
        {
            if (type.GetCustomAttribute<SingletonServiceAttribute>() is not null)
                builder.Services.AddSingleton(type);
            if (type.GetCustomAttribute<TransientServiceAttribute>() is not null)
                builder.Services.AddTransient(type);
        }
        return builder;
    }

    public static Task Navigate(Type type, object? args = null)
    {
        if (Application.Current.MainPage is not MainPage mainPage)
            return Task.CompletedTask;
        var page = ActivatePage(type, args);
        return mainPage.Navigation.PushAsync(page);
    }

    public static Task NavigateBack()
    {
        return Application.Current.MainPage is not MainPage mainPage
            ? Task.CompletedTask
            : mainPage.Navigation.PopAsync();
    }

    public static Page ActivatePage(Type type, object? args = null)
    {
        var page = (Page)GetService(type);
        var pageAttribute = type.GetCustomAttribute<AttachModelAttribute>();
        if (pageAttribute is not null)
        {
            var pageService = GetService(pageAttribute.Type);
            if (pageAttribute.Type.GetInterfaces().Contains(typeof(IInitializableObject)))
                ((IInitializableObject)pageService).Initialize(args);
            page.BindingContext = pageService;
        }
        return page;
    }

    public static TService? GetService<TService>()
    {
        #if WINDOWS
        return MauiWinUIApplication.Current.Services.GetService<TService>();
        #elif ANDROID
        return MauiApplication.Current.Services.GetService<TService>();
        #else
        return default;
        #endif
    }

    public static object? GetService(Type serviceType)
    {
        #if WINDOWS
        return MauiWinUIApplication.Current.Services.GetService(serviceType);
        #elif ANDROID
        return MauiApplication.Current.Services.GetService(serviceType);
        #else
        return default;
        #endif
    }

}
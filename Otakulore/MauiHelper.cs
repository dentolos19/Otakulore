using System.Reflection;
using AniListNet.Objects;
using Otakulore.Helpers;
using Otakulore.Models;
using Otakulore.Pages;
using UraniumUI;

namespace Otakulore;

public static class MauiHelper
{

    public static MauiAppBuilder SetupFonts(this MauiAppBuilder builder)
    {
        return builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("Poppins.ttf", "Poppins");
            fonts.AddFontAwesomeIconFonts();
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
            var pageAttribute = type.GetCustomAttribute<PageServiceAttribute>();
            if (pageAttribute is null)
                continue;
            switch (pageAttribute.Type)
            {
                case PageServiceType.Singleton:
                    builder.Services.AddSingleton(type);
                    break;
                case PageServiceType.Transient:
                    builder.Services.AddTransient(type);
                    break;
            }
        }
        return builder;
    }

    public static Page ActivatePage(Type pageType, object? args = null)
    {
        var pageService = GetService(pageType);
        if (pageService is not Page page)
            throw new Exception("The page service is unavailable.");
        var pageAttribute = pageType.GetCustomAttribute<PageServiceAttribute>();
        if (pageAttribute?.ModelType is null)
            return page;
        var pageModelService = GetService(pageAttribute.ModelType);
        if (pageModelService is not BasePageModel pageModel)
            throw new Exception("The specified page model is invalid.");
        page.NavigatedTo += (_, _) => pageModel.OnNavigatedTo();
        page.NavigatedFrom += (_, _) => pageModel.OnNavigatedFrom();
        pageModel.Activate(args);
        page.BindingContext = pageModel;
        return page;
    }

    public static Task Navigate(Type pageType, object? args = null)
    {
        if (Application.Current!.MainPage is not MainPage mainPage)
            return Task.CompletedTask;
        var page = ActivatePage(pageType, args);
        return mainPage.Navigation.PushAsync(page);
    }

    public static Task NavigateBack()
    {
        if (Application.Current!.MainPage is not MainPage mainPage)
            return Task.CompletedTask;
        return mainPage.Navigation.PopAsync();
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

    public static MediaSeason GetCurrentSeason(DateOnly date)
    {
        var value = date.Month + date.Day / 100f;
        if (value < 3.21 || value >= 12.22)
            return MediaSeason.Winter;
        if (value < 6.21)
            return MediaSeason.Spring;
        if (value < 9.23)
            return MediaSeason.Summer;
        return MediaSeason.Fall;
    }

    public static async Task<string> ReadTextAsset(string fileName)
    {
        await using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }

}
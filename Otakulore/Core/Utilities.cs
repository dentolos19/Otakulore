using HtmlAgilityPack;

namespace Otakulore.Core;

public static class Utilities
{

    private static HtmlWeb? _htmlWebInstance;

    public static HtmlWeb HtmlWeb => _htmlWebInstance ??= new HtmlWeb();

    public static string GetVersionString()
    {
        #if ANDROID
        var buildVersion = VersionTracking.CurrentBuild;
        #if DEBUG
        buildVersion = "Debug";
        #endif
        return $"{VersionTracking.CurrentVersion} ({buildVersion})";
        #else
        var version = VersionTracking.CurrentVersion;
        var buildVersion = version.Split('.')[3];
        #if DEBUG
        buildVersion = "Debug";
        #endif
        return $"{version.Remove(version.LastIndexOf("."))} ({buildVersion})";
        #endif
    }

}
using System;
using System.Diagnostics;
using Humanizer;

namespace Otakulore.Core
{

    public static class DebugLogger
    {

        private static string Time => $"{DateTime.Now:yyyy-MM-dd @ HH:mm:ss}";

        public static void PostLine(string content, LoggerStatus status = LoggerStatus.Information)
        {
            Debug.WriteLine($"[{Time} * {status.Humanize()}] {content}");
        }

        public static void PostChunk(string chunk)
        {
            Debug.WriteLine($"====> {Time} <====");
            Debug.WriteLine(chunk);
            Debug.WriteLine("=================================");
        }

    }

}
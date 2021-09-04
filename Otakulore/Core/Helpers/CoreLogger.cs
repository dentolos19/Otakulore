using Humanizer;
using System;
using System.Diagnostics;
using System.IO;

namespace Otakulore.Core.Helpers
{

    public static class CoreLogger
    {

        private static string Time => $"{DateTime.Now:yyyy-MM-dd @ HH:mm:ss}";

        private static string LogHistory { get; set; }

        public static void PostLine(string message, LoggerStatus status = LoggerStatus.Information)
        {
            var content = $"[{Time} * {status.Humanize()}] {message}";
            Debug.WriteLine(content);
            LogHistory += content + Environment.NewLine;
        }

        public static void PostChunk(string chunk)
        {
            var content = $"====> {Time} <====" + Environment.NewLine +
                          chunk + Environment.NewLine +
                          "=================================";
            Debug.WriteLine(content);
            LogHistory += content + Environment.NewLine;
        }

        public static void SaveToFile(string filePath)
        {
            PostLine("Saved log file to " + filePath);
            File.WriteAllText(filePath, LogHistory);
        }

    }

}
using Humanizer;
using System;
using System.Diagnostics;
using System.IO;

namespace Otakulore.Core.Helpers
{

    public static class BasicLogger
    {

        private static string Time => $"{DateTime.Now:yyyy-MM-dd @ HH:mm:ss}";

        private static string LogData { get; set; }

        public static void PostLine(string message, LoggerStatus status = LoggerStatus.Information)
        {
            var content = $"[{Time} * {status.Humanize()}] {message}";
            Debug.WriteLine(content);
            LogData += Environment.NewLine + content;
        }

        public static void PostChunk(string chunk)
        {
            var content = $"====> {Time} <====" + Environment.NewLine +
                          chunk + Environment.NewLine +
                          "=================================";
            Debug.WriteLine(content);
            LogData += Environment.NewLine + content;
        }

        public static void SaveToFile(string filePath)
        {
            File.WriteAllText(filePath, LogData);
        }

    }

}
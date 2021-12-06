using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MdXaml;
using ModernWpf.Controls;

namespace Otakulore.Core;

public static class Utilities
{

    public static ContentDialog CreateExceptionDialog(Exception exception, string? message = null)
    {
        var markdown =
            $"{(string.IsNullOrEmpty(message) ? "The exception was unhandled!" : message)}" +
            $"{Environment.NewLine}{Environment.NewLine}" +
            $"{exception.Message}" +
            $"{Environment.NewLine}{Environment.NewLine}" +
            $"```{Environment.NewLine}" +
            $"{exception.StackTrace}" +
            $"{Environment.NewLine}```";
        return new ContentDialog
        {
            Title = "An internal exception occurred!",
            Content = new FlowDocumentScrollViewer
            {
                Document = new Markdown().Transform(markdown),
                FontFamily = new FontFamily("Consolas"),
                BorderThickness = new Thickness(0)
            },
            CloseButtonText = "Okay"
        };
    }

}
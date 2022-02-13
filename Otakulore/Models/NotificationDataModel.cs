using System;

namespace Otakulore.Models;

public class NotificationDataModel
{

    public string Message { get; init; }
    public string? ContinueText { get; init; }

    public event EventHandler? ContinueClicked;

    public void InvokeContinueClicked()
    {
        ContinueClicked?.Invoke(this, EventArgs.Empty);
    }

}
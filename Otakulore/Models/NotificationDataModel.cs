using System;

namespace Otakulore.Models;

public class NotificationDataModel
{

    public string Message { get; set; }
    public string? ContinueText { get; set; }

    public event EventHandler? ContinueClicked;

    public void InvokeContinueClicked()
    {
        ContinueClicked?.Invoke(this, EventArgs.Empty);
    }

}
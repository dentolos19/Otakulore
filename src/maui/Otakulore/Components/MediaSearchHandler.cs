using Otakulore.Pages;

namespace Otakulore.Components;

public class MediaSearchHandler : SearchHandler
{

    protected override async void OnQueryConfirmed()
    {
        await Shell.Current.GoToAsync(nameof(SearchPage), new Dictionary<string, object>
        {
            { "query", Query }
        });
    }

}
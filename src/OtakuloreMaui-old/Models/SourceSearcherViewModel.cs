using CommunityToolkit.Mvvm.ComponentModel;

namespace Otakulore.Models;

public class SourceSearcherViewModel : ObservableObject, IQueryAttributable
{

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("query"))
            return;
    }

}
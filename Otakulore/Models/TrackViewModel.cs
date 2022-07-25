using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class TrackViewModel : ObservableObject, IQueryAttributable
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("id"))
            return;
        if (query["id"] is not int id)
            return;
        // TODO: implement tracking
    }

}
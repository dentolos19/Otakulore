using System.Collections.ObjectModel;
using Otakulore.Controls;

namespace Otakulore.Models;

public class SearchViewModel : BaseViewModel
{

    private string _query;
    private ResultIndicatorState _state = ResultIndicatorState.None;

    public string Query
    {
        get => _query;
        set => UpdateProperty(ref _query, value);
    }

    public ResultIndicatorState State
    {
        get => _state;
        set => UpdateProperty(ref _state, value);
    }

    public ObservableCollection<MediaItemModel> Items { get; } = new();

    public async void Search()
    {
        State = ResultIndicatorState.Loading;
        Items.Clear();
        var result = await App.Client.SearchMedia(_query);
        if (result is { Length: > 0 })
        {
            State = ResultIndicatorState.None;
            foreach (var entry in result)
                Items.Add(new MediaItemModel(entry));
        }
        else
        {
            State = ResultIndicatorState.NoResult;
        }
    }

}
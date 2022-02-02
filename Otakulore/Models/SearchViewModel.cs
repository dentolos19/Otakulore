using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Otakulore.Controls;

namespace Otakulore.Models;

public class SearchViewModel : BaseViewModel
{

    private string _query;
    private string _text;
    private ResultIndicatorState _state = ResultIndicatorState.None;

    public string Query
    {
        get => _query;
        set => UpdateProperty(ref _query, value);
    }

    public string Text
    {
        get => _text;
        set => UpdateProperty(ref _text, value);
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
        try
        {
            var result = await App.Client.SearchMedia(_query);
            if (result is not { Length: > 0 })
            {
                Text = "Search yielded no results";
                State = ResultIndicatorState.NoResult;
                return;
            }
            State = ResultIndicatorState.None;
            foreach (var entry in result)
                Items.Add(new MediaItemModel(entry));
        }
        catch (Exception exception)
        {
            Text = $"Search yielded an error: {exception.Message}";
            State = ResultIndicatorState.NoResult;
        }
    }

}
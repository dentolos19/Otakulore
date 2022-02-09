using System.Collections.ObjectModel;
using Otakulore.Controls;
using Otakulore.Core;

namespace Otakulore.Models;

public class CinemaViewModel : BaseViewModel
{

    private string _url;
    private ResultIndicatorState _state;

    public string Url
    {
        get => _url;
        set => UpdateProperty(ref _url, value);
    }

    public ResultIndicatorState State
    {
        get => _state;
        set => UpdateProperty(ref _state, value);
    }

    public ObservableCollection<ContentItemModel> Items { get; } = new();

    public void Load(IProvider provider, MediaSource source)
    {
        State = ResultIndicatorState.Loading;
        var contents = provider switch
        {
            IAnimeProvider animeProvider => animeProvider.GetContents(source),
            IMangaProvider mangaProvider => mangaProvider.GetContents(source)
        };
        foreach (var content in contents)
            Items.Add(new ContentItemModel(content));
        State = ResultIndicatorState.None;
    }

}
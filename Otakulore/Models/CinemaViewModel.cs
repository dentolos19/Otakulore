using System.Collections.ObjectModel;
using Otakulore.Core;

namespace Otakulore.Models;

public class CinemaViewModel : BaseViewModel
{

    private string _url;

    public string Url
    {
        get => _url;
        set => UpdateProperty(ref _url, value);
    }

    public ObservableCollection<ContentItemModel> Items { get; } = new();

    public void Load(IProvider provider, MediaSource source)
    {
        var contents = provider switch
        {
            IAnimeProvider animeProvider => animeProvider.GetContents(source),
            IMangaProvider mangaProvider => mangaProvider.GetContents(source)
        };
        foreach (var content in contents)
            Items.Add(new ContentItemModel(content));
    }

}
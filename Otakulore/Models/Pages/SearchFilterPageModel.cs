using System.Collections.ObjectModel;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Helpers;
using Otakulore.Pages;

namespace Otakulore.Models;

[TransientService]
public partial class SearchFilterPageModel : ObservableObject, IQueryAttributable
{

    private SearchMediaFilter _filter = new();

    [ObservableProperty] private bool _typeEnabled;
    [ObservableProperty] private MediaType _type = MediaType.Anime;
    [ObservableProperty] private bool _onList;
    [ObservableProperty] private ObservableCollection<MediaType> _types = new();

    public SearchFilterPageModel()
    {
        foreach (var @enum in (MediaType[])Enum.GetValues(typeof(MediaType)))
            Types.Add(@enum);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("filter"))
            return;
        if (query["filter"] is not SearchMediaFilter filter)
            return;
        _filter = filter;
        if (_filter.Type.HasValue)
            Type = _filter.Type.Value;
        if (_filter.OnList.HasValue)
            OnList = _filter.OnList.Value;
    }

    [RelayCommand]
    private Task Filter()
    {
        _filter.Type = TypeEnabled ? Type : null;
        _filter.OnList = OnList;
        return Shell.Current.GoToAsync($"../{nameof(SearchPage)}", new Dictionary<string, object>
        {
            { "filter", _filter }
        });
    }

    [RelayCommand]
    private Task Reset()
    {
        return Shell.Current.GoToAsync($"../{nameof(SearchPage)}", new Dictionary<string, object>
        {
            { "filter", new SearchMediaFilter { Query = _filter.Query, Sort = _filter.Sort } }
        });
    }

    [RelayCommand]
    private Task Cancel()
    {
        return MauiHelper.NavigateBack();
    }

}
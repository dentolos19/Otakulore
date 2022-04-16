using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

[ObservableObject]
public partial class SearchViewModel
{

    private readonly AniClient _client = new();

    [ObservableProperty] private string _query;
    [ObservableProperty] private bool _isLoading;

    public ObservableCollection<SearchItemModel> Items { get; }
    public ICommand SearchCommand { get; }

    public SearchViewModel()
    {
        Items = new ObservableCollection<SearchItemModel>();
        SearchCommand = new Command(async () =>
        {
            IsLoading = true;
            Items.Clear();
            var results = await _client.SearchMedia(new AniFilter { Query = _query });
            foreach (var item in results.Data)
                Items.Add(new SearchItemModel
                {
                    Id = item.Id,
                    ImageUrl = item.Cover.ExtraLargeImageUrl,
                    Title = item.Title.Preferred
                });
            IsLoading = false;
        }, () => !string.IsNullOrEmpty(_query));
    }

}
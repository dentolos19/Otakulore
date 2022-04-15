using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using JikanDotNet;

namespace Otakulore.Models;

[ObservableObject]
public partial class SearchViewModel
{

    private readonly IJikan _client = new Jikan();

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
            var results = await _client.SearchAnimeAsync(_query);
            foreach (var item in results.Data)
                Items.Add(new SearchItemModel
                {
                    Id = (long)item.MalId,
                    ImageUrl = item.Images.JPG.ImageUrl,
                    Title = item.Title
                });
            IsLoading = false;
        }, () => !string.IsNullOrEmpty(_query));
    }

}
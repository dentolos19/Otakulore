using AniListNet;
using AniListNet.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;
using Otakulore.Services;
using Otakulore.Utilities;
using Otakulore.Utilities.Attributes;

namespace Otakulore.Models;

[TransientService]
public partial class SearchPageModel : BasePageModel
{

    private SearchMediaFilter _filter = new();

    [ObservableProperty] private AccumulableCollection<MediaItemModel> _items = new();

    protected override void Initialize(object? args = null)
    {
        if (args is string query)
            _filter.Query = query;
        if (args is SearchMediaFilter filter)
            _filter = filter;
        if (ParentPage is SearchPage page)
            page.SearchBox.Text = _filter.Query;
        SearchCommand.Execute(_filter.Query);
    }

    [RelayCommand]
    private Task Search(string query)
    {
        _filter.Query = query;
        Items = new AccumulableCollection<MediaItemModel>();
        Items.AccumulationFunc += async index =>
        {
            var result = await DataService.Instance.Client.SearchMediaAsync(_filter, new AniPaginationOptions(index));
            return (
                result.HasNextPage,
                result.Data.Select(MediaItemModel.Map).ToList()
            );
        };
        Items.AccumulateCommand.Execute(null);
        return Task.CompletedTask;
    }

    [RelayCommand]
    private Task Filter()
    {
        return MauiHelper.Navigate(typeof(SearchFilterPage), _filter);
    }

}
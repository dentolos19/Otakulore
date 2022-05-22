﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Otakulore.Models;

public partial class SearchViewModel : ObservableObject, IQueryAttributable
{

    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("query"))
            SearchCommand.Execute(query["query"].ToString());
    }

    [ICommand]
    private async Task Search(string query)
    {
        Items.Clear();
        IsLoading = true;
        var results = await App.Client.SearchMediaAsync(query);
        foreach (var data in results.Data)
            Items.Add(new MediaItemModel(data));
        IsLoading = false;
    }

}
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class CharacterDetailsPageModel : ObservableObject, IQueryAttributable
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();

    private int _id;

    [ObservableProperty] private Uri _imageUrl;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _favorites;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _gender;
    [ObservableProperty] private string _dateOfBirth;
    [ObservableProperty] private bool _isFavorite;
    [ObservableProperty] private bool _isLoading = true;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("id"))
            return;
        if (query["id"] is not int id)
            return;
        _id = id;
        var character = await _data.Client.GetCharacterAsync(_id);
        ImageUrl = character.Image.LargeImageUrl;
        Name = character.Name.PreferredName;
        Favorites = "❤️ " + character.Favorites;
        Description = character.Description ?? "No description provided.";
        Gender = character.Gender ?? "Unknown";
        DateOfBirth = character.DateOfBirth.ToDateTime()?.ToShortDateString() ?? "Unknown";
        IsFavorite = character.IsFavorite;
        IsLoading = false;
    }

    [RelayCommand]
    private async Task ToggleFavorite()
    {
        if (_data.Client.IsAuthenticated)
            IsFavorite = await _data.Client.ToggleCharacterFavoriteAsync(_id);
        else
            await Toast.Make("You need to login into AniList via the settings to access this feature.").Show();
    }

}
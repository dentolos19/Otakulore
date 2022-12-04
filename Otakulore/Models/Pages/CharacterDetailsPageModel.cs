using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class CharacterDetailsPageModel : BasePageModel
{

    private readonly ExternalService _externalService = MauiHelper.GetService<ExternalService>();

    private int _id;

    [ObservableProperty] private Uri _imageUrl;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _favorites;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _gender;
    [ObservableProperty] private string _birthday;

    public override async void Initialize(object? args = null)
    {
        if (args is not int id)
            return;
        _id = id;
        var character = await _externalService.AniClient.GetCharacterAsync(_id);
        ImageUrl = character.Image.LargeImageUrl;
        Name = character.Name.PreferredName;
        Favorites = "❤️ " + character.Favorites;
        Description = character.Description ?? "No description provided.";
        Gender = character.Gender ?? "Unknown";
        Birthday = character.DateOfBirth.ToDateTime()?.ToShortDateString() ?? "Unknown";
    }

}
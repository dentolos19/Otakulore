using CommunityToolkit.Mvvm.ComponentModel;
using Markdig;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class CharacterDetailsPageModel : BasePageModel
{

    private int _id;

    [ObservableProperty] private Uri _imageUrl;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _favorites;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _gender;
    [ObservableProperty] private string _birthday;

    protected override async void Initialize(object? args = null)
    {
        if (args is not int id)
            return;
        _id = id;
        var character = await DataService.Instance.Client.GetCharacterAsync(_id);
        ImageUrl = character.Image.LargeImageUrl;
        Name = character.Name.PreferredName;
        Favorites = "❤️ " + character.Favorites;
        Description = Markdown.ToHtml(character.Description ?? "No description provided.");
        Gender = character.Gender ?? "Unknown";
        Birthday = character.DateOfBirth.ToDateTime()?.ToShortDateString() ?? "Unknown";
    }

}
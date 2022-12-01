using AniListNet.Objects;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class CharacterItemModel
{

    public int Id { get; }
    public Uri ImageUrl { get; }
    public string Name { get; }
    public string? Role { get; }

    public CharacterItemModel(Character character)
    {
        Id = character.Id;
        ImageUrl = character.Image.LargeImageUrl;
        Name = character.Name.FullName;
    }

    public CharacterItemModel(CharacterEdge character)
    {
        Id = character.Character.Id;
        ImageUrl = character.Character.Image.LargeImageUrl;
        Name = character.Character.Name.FullName;
        Role = character.Role.ToString();
    }

    [RelayCommand]
    private Task Open()
    {
        return MauiHelper.Navigate(
            typeof(CharacterDetailsPage),
            new Dictionary<string, object>
            {
                { "id", Id }
            }
        );
    }

}
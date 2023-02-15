using AniListNet.Objects;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class CharacterItemModel
{
    public required int Id { get; init; }
    public required Uri ImageUrl { get; init; }
    public required string Name { get; init; }
    public required string Role { get; init; }

    [RelayCommand]
    private Task Interact()
    {
        return MauiHelper.Navigate(typeof(CharacterDetailsPage), Id);
    }

    public static CharacterItemModel Map(CharacterEdge character)
    {
        return new CharacterItemModel
        {
            Id = character.Character.Id,
            ImageUrl = character.Character.Image.LargeImageUrl,
            Name = character.Character.Name.FullName,
            Role = character.Role.ToString()
        };
    }
}
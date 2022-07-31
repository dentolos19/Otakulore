using AniListNet.Objects;

namespace Otakulore.Models;

public class CharacterItemModel
{

    public Uri ImageUrl { get; }
    public string Name { get; }
    public string Role { get; }

    public CharacterItemModel(CharacterEdge character)
    {
        ImageUrl = character.Character.Image.LargeImageUrl;
        Name = character.Character.Name.FullName;
        Role = character.Role.ToString();
    }

}
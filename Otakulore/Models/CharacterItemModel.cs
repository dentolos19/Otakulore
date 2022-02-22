using Otakulore.Core.AniList;

namespace Otakulore.Models;

public class CharacterItemModel
{

    public CharacterEdge Character { get; }

    public CharacterItemModel(CharacterEdge character)
    {
        Character = character;
    }

}
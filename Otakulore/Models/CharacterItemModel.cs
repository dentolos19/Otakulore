using System.Collections.Generic;
using CommunityToolkit.WinUI.UI.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public class CharacterItemModel
{

    public CharacterEdge Character { get; }
    public string Tag { get; }
    public IList<MetadataItem> Meta { get; } = new List<MetadataItem>();

    public CharacterItemModel(CharacterEdge character)
    {
        Character = character;
        var role = Character.Role.ToEnumDescription(true);
        ;
        Tag = role;
        Meta.Add(new MetadataItem { Label = role });
        if (Character.Details.Gender != null)
            Meta.Add(new MetadataItem { Label = Character.Details.Gender });
        if (Character.Details.Age != null)
            Meta.Add(new MetadataItem { Label = Character.Details.Age });
    }

}
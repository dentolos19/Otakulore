using AniListNet.Objects;
using Humanizer;

namespace Otakulore.Models;

public class MediaRelationItemModel : MediaItemModel
{

    public MediaRelationItemModel(MediaEdge media) : base(media.Media)
    {
        Tag = media.Relation.Humanize(LetterCasing.Title);
    }

}
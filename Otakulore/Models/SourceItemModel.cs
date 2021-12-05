using Otakulore.Core;
using Otakulore.Services;

namespace Otakulore.Models;

public class SourceItemModel
{

    public MediaType Type { get; init; }
    public string ImageUrl { get; init; }
    public string Title { get; init; }
    public IMediaInfo Info { get; init; }

}
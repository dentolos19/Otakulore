using Otakulore.Services;

namespace Otakulore.Core;

public class ObjectData
{

    public MediaType? MediaType { get; init; }
    public IMediaInfo? MediaInfo { get; init; }
    public long? Id { get; init; }
    public IProvider? Provider { get; init; }
    public string? Query { get; init; }

}
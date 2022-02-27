namespace Otakulore.Core.AniList;

public class AniFilter
{

    public string? Query { get; set; }
    public MediaSort Sort { get; set; } = MediaSort.Relevance;
    public MediaType? Type { get; set; }
    public IList<string> Genres { get; set; } = new List<string>();
    public IList<string> Tags { get; set; } = new List<string>();

}
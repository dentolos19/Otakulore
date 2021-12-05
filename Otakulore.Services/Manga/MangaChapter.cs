namespace Otakulore.Services.Manga;

public class MangaChapter : IMediaContent
{

    public int Index { get; init; }
    public string? Title { get; init; }

    public string Url { get; init; }

}
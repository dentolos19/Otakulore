namespace Otakulore.Models;

public class DetailsViewModel : BaseViewModel
{

    private bool _isFavorite;

    public long Id { get; set; }
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    public string Synopsis { get; set; }
    public string? Background { get; set; }
    public string Format { get; set; }
    public string Status { get; set; }
    public string Episodes { get; set; }

    public bool IsFavorite
    {
        get => _isFavorite;
        set => UpdateProperty(ref _isFavorite, value);
    }

}
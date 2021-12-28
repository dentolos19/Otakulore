using Otakulore.Core;
using System;

namespace Otakulore.ViewModels;

public class DetailsViewModel : BaseViewModel
{

    private bool _isFavorite;

    public MediaType Type { get; init; }
    public long? Id { get; init; }

    public Uri ImageUrl { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Synopsis { get; set; }
    public string? Background { get; set; }
    public string Format { get; set; }
    public string Status { get; set; }
    public string Contents { get; set; }

    public bool IsFavorite
    {
        get => _isFavorite;
        set => UpdateProperty(ref _isFavorite, value);
    }

}
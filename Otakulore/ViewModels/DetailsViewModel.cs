using Otakulore.Core;

namespace Otakulore.ViewModels;

public class DetailsViewModel : BaseViewModel
{

    public MediaType Type { get; init; }
    public long Id { get; init; }
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    public string? Background { get; set; }
    public string Format { get; set; }
    public string Status { get; set; }

}
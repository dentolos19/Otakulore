using Otakulore.Models;

namespace Otakulore.Pages;

public partial class SeasonalPage
{

    private SeasonalViewModel Context => (SeasonalViewModel)BindingContext;

    public MediaSeason Season { get; set; }

    public SeasonalPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (Context.CurrentSeason == null)
            Context.SetSeason(Season);
    }

}
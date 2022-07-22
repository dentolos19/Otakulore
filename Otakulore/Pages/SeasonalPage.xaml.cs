using AniListNet.Objects;
using Otakulore.Models;

namespace Otakulore.Pages;

public partial class SeasonalPage
{

    private bool _hasNavigated;

    private SeasonalViewModel Context => (SeasonalViewModel)BindingContext;

    public MediaSeason Season { get; set; }

    public SeasonalPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<SeasonalViewModel>();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (_hasNavigated)
            return;
        _hasNavigated = true;
        Context.Season = Season;
        Context.AccumulateCommand.Execute(null);
    }

}
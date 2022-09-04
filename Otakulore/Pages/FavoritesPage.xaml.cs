using Otakulore.Models;

namespace Otakulore.Pages;

public partial class FavoritesPage
{

    private FavoritesPageModel Context => (FavoritesPageModel)BindingContext;

    public FavoritesPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<FavoritesPageModel>();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await Context.CheckAuthenticationStatus();
    }

}
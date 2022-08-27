using Otakulore.Models;

namespace Otakulore.Pages;

public partial class LibraryPage
{

    private LibraryPageModel Context => (LibraryPageModel)BindingContext;

    public LibraryPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<LibraryPageModel>();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await Context.CheckAuthenticationStatus();
    }

}
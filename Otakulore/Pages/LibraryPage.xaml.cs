using Otakulore.Models;

namespace Otakulore.Pages;

public partial class LibraryPage
{

    private LibraryViewModel Context => (LibraryViewModel)BindingContext;

    public LibraryPage()
    {
        InitializeComponent();
        BindingContext = MauiHelper.GetService<LibraryViewModel>();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await Context.CheckAuthenticationStatus();
    }

}
using Otakulore.Models;

namespace Otakulore.Pages;

public partial class SearchProviderPage
{

    private SearchProviderViewModel Context => (SearchProviderViewModel)BindingContext;

    public SearchProviderPage()
    {
        InitializeComponent();
    }

    private async void OnSelectionIndexChanged(object sender, EventArgs args)
    {
        await Context.SearchCommand.ExecuteAsync(null);
    }

}
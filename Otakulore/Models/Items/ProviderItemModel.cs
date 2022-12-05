using CommunityToolkit.Mvvm.Input;
using Otakulore.Content;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class ProviderItemModel
{

    public required string Name { get; init; }
    public required IProvider Provider { get; init; }

    [RelayCommand]
    private Task Interact()
    {
        return MauiHelper.Navigate(typeof(SearchProviderPage));
    }

    public static ProviderItemModel Map(IProvider provider)
    {
        return new ProviderItemModel
        {
            Name = provider.Name,
            Provider = provider
        };
    }

}
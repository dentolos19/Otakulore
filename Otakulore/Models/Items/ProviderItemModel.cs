using CommunityToolkit.Mvvm.Input;
using Otakulore.Content;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class ProviderItemModel
{

    public string Name { get; }
    public IProvider Provider { get; }

    public ProviderItemModel(IProvider provider)
    {
        Name = provider.Name;
        Provider = provider;
    }

    [RelayCommand]
    private Task Open()
    {
        return MauiHelper.Navigate(
            typeof(SearchProviderPage),
            new Dictionary<string, object>
            {
                { "provider", Provider }
            }
        );
    }

}
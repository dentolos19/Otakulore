using System.Collections.ObjectModel;
using Otakulore.Models;

namespace Otakulore.ViewModels;

public class FavoriteViewModel : BaseViewModel
{

    public ObservableCollection<MediaItemModel> Favorites { get; } = new();

}
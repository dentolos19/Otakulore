using Otakulore.Models;
using System.Collections.ObjectModel;

namespace Otakulore.ViewModels
{

    public class PlayerReaderViewModel : LoadingViewModel
    {
        
        public string Title { get; set; }
        public ObservableCollection<EpisodeChapterItemModel> EpisodeChapterList { get; set; } = new ObservableCollection<EpisodeChapterItemModel>();

        public static PlayerReaderViewModel CreateViewModel(ChannelItemModel model)
        {
            var viewModel = new PlayerReaderViewModel { Title = model.Title };
            if (model.AnimeProvider != null)
                viewModel.Title += $" | {model.AnimeProvider.Name}";
            if (model.MangaProvider != null)
                viewModel.Title += $" | {model.MangaProvider.Name}";
            return viewModel;
        }

    }

}
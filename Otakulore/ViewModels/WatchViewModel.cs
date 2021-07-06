using Otakulore.Models;
using System.Collections.ObjectModel;

namespace Otakulore.ViewModels
{

    public class WatchViewModel : LoadingViewModel
    {
        
        public string Title { get; set; }
        public ObservableCollection<EpisodeItemModel> EpisodeList { get; set; } = new ObservableCollection<EpisodeItemModel>();

        public static WatchViewModel CreateViewModel(WatchItemModel model)
        {
            return new WatchViewModel
            {
                Title = model.Title + " | " + model.Provider.Name
            };
        }

    }

}
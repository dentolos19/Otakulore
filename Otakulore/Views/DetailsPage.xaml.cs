using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Navigation;
using Otakulore.Core;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class DetailsPage
{

    private readonly BackgroundWorker _detailsWorker;

    private DetailsViewModel ViewModel => (DetailsViewModel)DataContext;

    public DetailsPage()
    {
        _detailsWorker = new BackgroundWorker();
        _detailsWorker.DoWork += async (_, args) =>
        {
            if (args.Argument is not KeyValuePair<MediaType, long>(var type, var id))
                return;
            var viewModel = new DetailsViewModel { Type = type, Id = id };
            if (type == MediaType.Anime)
            {
                var details = await App.JikanService.GetAnime(id);
                viewModel.ImageUrl = details.ImageURL;
                viewModel.Title = details.Title;
                viewModel.Synopsis = details.Synopsis;
                viewModel.Background = details.Background;
                viewModel.Format = details.Type;
                viewModel.Status = details.Status;
            }
            else if (type == MediaType.Manga)
            {
                var details = await App.JikanService.GetManga(id);
                viewModel.ImageUrl = details.ImageURL;
                viewModel.Title = details.Title;
                viewModel.Synopsis = details.Synopsis;
                viewModel.Background = details.Background;
                viewModel.Format = details.Type;
                viewModel.Status = details.Status;
            }
            Dispatcher.Invoke(() => DataContext = viewModel);
        };
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.ExtraData is not KeyValuePair<MediaType, long> data)
            return;
        _detailsWorker.RunWorkerAsync(data);
    }

}
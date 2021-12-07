using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Navigation;
using Otakulore.Core;
using Otakulore.Services;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class AnimePlayerPage
{

    private readonly BackgroundWorker _contentLoader;

    private IAnimeProvider? _provider;

    private AnimePlayerViewModel ViewModel => (AnimePlayerViewModel)DataContext;

    public AnimePlayerPage()
    {
        _contentLoader = new BackgroundWorker();
        _contentLoader.DoWork += (_, args) =>
        {
            // TODO: load content
        };
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.ExtraData is not ObjectData data)
            return;
        ViewModel.Title = data.MediaInfo.Title;
        _provider = (IAnimeProvider)data.Provider;
        _contentLoader.RunWorkerAsync(data.MediaInfo);
    }

    private void OnEpisodeChanged(object sender, SelectionChangedEventArgs args)
    {

    }

}
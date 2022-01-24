using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;

namespace Otakulore.Views.Dialogs;

public sealed partial class SelectProviderDialog
{

    private readonly MediaType _type;

    public IProvider? Result;

    public SelectProviderDialog(MediaType type)
    {
        _type = type;
        XamlRoot = App.MainWindow.Content.XamlRoot;
        InitializeComponent();
    }

    private void OnDialogOpened(ContentDialog sender, ContentDialogOpenedEventArgs args)
    {
        foreach (var provider in App.Providers)
            switch (_type)
            {
                case MediaType.Anime when provider is IAnimeProvider:
                case MediaType.Manga when provider is IMangaProvider:
                    ProviderList.Items.Add(new ProviderItemModel(provider));
                    break;
            }
    }

    private void OnProviderClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is not ProviderItemModel item)
            return;
        Result = item.Provider;
        Hide();
    }

}
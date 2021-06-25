using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core.Anime;
using Otakulore.Core.Anime.Providers;
using Otakulore.Core.Kitsu;
using Otakulore.Models;
using Otakulore.ViewModels;

namespace Otakulore.Views
{
    
    public sealed partial class DetailsView
    {
        
        public DetailsView()
        {
            InitializeComponent();
            NavigationView.SelectedItem = NavigationView.MenuItems.OfType<NavigationViewItem>().First();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is KitsuData data))
                return;
            DataContext = DetailsViewModel.CreateViewModel(data);
            ContentSearchInput.Text = data.Attributes.CanonicalTitle;
            var titles = new List<TitleItemModel>();
            foreach (var (languageCode, title) in data.Attributes.Titles)
            {
                if (string.IsNullOrEmpty(languageCode) || string.IsNullOrEmpty(title))
                    return;
                string languageName = null;
                try
                {
                    languageName = CultureInfo.GetCultureInfo(languageCode).DisplayName;
                }
                catch
                {
                    // do nothing
                }
                if (string.IsNullOrEmpty(languageName))
                    languageName = languageCode;
                titles.Add(new TitleItemModel
                {
                    LanguageName = languageName,
                    Title = title
                });
            }
            ContentSearchInput.ItemsSource = titles;
        }

        private void SwitchView(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (!(args.SelectedItem is NavigationViewItem selectedItem))
                return;
            if (selectedItem.Content.ToString() == "Watch" && ProviderSelection.SelectedIndex < 0)
                ProviderSelection.SelectedIndex = 0;
            NavigationView.Content = selectedItem.Tag;
        }

        private void ContentSearchEntered(object sender, KeyRoutedEventArgs args)
        {
            if (args.Key != VirtualKey.Enter)
                return;
            UpdateWatchContent(null, null);
        }

        private void ContentSearchChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (!(args.SelectedItem is TitleItemModel model))
                return;
            sender.Text = model.Title;
            UpdateWatchContent(null, null);
        }

        private void UpdateWatchContent(object sender, SelectionChangedEventArgs args)
        {
            if (!(ProviderSelection.SelectedItem is ComboBoxItem item))
                return;
            var query = ContentSearchInput.Text;
            if (string.IsNullOrEmpty(query))
                return;
            var serviceCode = (string)item.Tag;
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ((DetailsViewModel)DataContext).IsLoading = true);
                AnimeInfo[] content;
                AnimeProvider provider;
                if (serviceCode == "4a")
                {
                    content = FourAnimeProvider.ScrapeSearchAnime(query);
                    provider = AnimeProvider.FourAnime;
                }
                else if (serviceCode == "ggo")
                {
                    content = GogoanimeProvider.ScrapeSearchAnime(query);
                    provider = AnimeProvider.Gogoanime;
                }
                else if (serviceCode == "ak")
                {
                    content = AnimeKisaProvider.ScrapeSearchAnime(query);
                    provider = AnimeProvider.AnimeKisa;
                }
                else
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        ((DetailsViewModel)DataContext).IsLoading = false;
                        await new MessageDialog("The selected provider is unavailable.").ShowAsync();
                    });
                    return;
                }
                if (content != null && content.Length > 0)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        WatchContentList.Items.Clear();
                        foreach (var channel in content)
                            WatchContentList.Items.Add(new WatchItemModel
                            {
                                ImageUrl = channel.ImageUrl,
                                Title = channel.Title,
                                EpisodesUrl = channel.EpisodesUrl,
                                Provider = provider
                            });
                    });
                }
                else
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await new MessageDialog("Unable to find available content with the current provider.").ShowAsync());
                }
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ((DetailsViewModel)DataContext).IsLoading = false);
            });
        }

        private void WatchSelectedContent(object sender, ItemClickEventArgs args)
        {
            if (args.ClickedItem is WatchItemModel model)
                Frame.Navigate(typeof(WatchView), model);
        }

    }

}
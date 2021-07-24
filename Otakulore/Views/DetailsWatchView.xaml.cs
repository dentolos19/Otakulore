using Otakulore.Core;
using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Common;
using Otakulore.Models;
using Otakulore.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Otakulore.Views
{

    public sealed partial class DetailsWatchView
    {

        private readonly BackgroundWorker _contentLoader;

        private string _id;

        public DetailsWatchView()
        {
            InitializeComponent();
            _contentLoader = new BackgroundWorker();
            _contentLoader.DoWork += LoadContent;
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is CommonMediaDetails data))
                return;
            _id = data.KitsuId.ToString();
            var titles = new List<TitleItemModel>();
            foreach (var (culture, title) in data.AlternativeTitles)
            {
                if (string.IsNullOrEmpty(culture) || string.IsNullOrEmpty(title))
                    return;
                string languageName = null;
                try
                {
                    languageName = CultureInfo.GetCultureInfo(culture).DisplayName;
                }
                catch
                {
                    // do nothing
                }
                if (string.IsNullOrEmpty(languageName))
                    languageName = culture;
                titles.Add(new TitleItemModel
                {
                    LanguageName = languageName,
                    Title = title
                });
            }
            SearchInput.ItemsSource = titles;
            SearchInput.Text = data.CanonicalTitle;
            foreach (var provider in ServiceUtilities.GetAnimeProviders())
            {
                var providerItem = new ComboBoxItem
                {
                    Content = provider.Name,
                    Tag = provider
                };
                ProviderSelection.Items.Add(providerItem);
            }
            if (ProviderSelection.SelectedIndex < 0)
                ProviderSelection.SelectedItem = ProviderSelection.Items.OfType<ComboBoxItem>().FirstOrDefault(item => ((IAnimeProvider)item.Tag).Id == App.Settings.DefaultAnimeProvider);
        }

        private async void LoadContent(object sender, DoWorkEventArgs args)
        {
            if (!(args.Argument is KeyValuePair<string, IAnimeProvider> parameters))
                return;
            var query = parameters.Key;
            var provider = parameters.Value;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ((LoadingViewModel)DataContext).IsLoading = true);
            var content = provider.SearchAnime(query);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                if (content != null && content.Length > 0)
                {
                    ContentList.Items.Clear();
                    foreach (var channel in content)
                        ContentList.Items.Add(new ChannelItemModel
                        {
                            Id = _id,
                            ImageUrl = channel.ImageUrl,
                            Title = channel.Title,
                            Url = channel.Url,
                            AnimeProvider = provider
                        });
                }
                else
                {
                    await new MessageDialog("Unable to find available content with the current provider.").ShowAsync();
                }
                ((LoadingViewModel)DataContext).IsLoading = false;
            });
        }

        private void SearchEntered(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            UpdateContent(null, null);
        }

        private void ProviderSelected(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (!(args.SelectedItem is TitleItemModel model))
                return;
            sender.Text = model.Title;
            UpdateContent(null, null);
        }

        private async void UpdateContent(object sender, SelectionChangedEventArgs args)
        {
            if (!(ProviderSelection.SelectedItem is ComboBoxItem item))
                return;
            var query = SearchInput.Text;
            if (string.IsNullOrEmpty(query))
                return;
            if (item.Tag is IAnimeProvider provider)
            {
                _contentLoader.RunWorkerAsync(new KeyValuePair<string, IAnimeProvider>(query, provider));
            }
            else
            {
                ((LoadingViewModel)DataContext).IsLoading = false;
                await new MessageDialog("The selected provider is unavailable.").ShowAsync();
            }
        }

        private void WatchContent(object sender, ItemClickEventArgs args)
        {
            if (args.ClickedItem is ChannelItemModel model)
                Frame.Navigate(typeof(AnimePlayerView), model);
        }

    }

}
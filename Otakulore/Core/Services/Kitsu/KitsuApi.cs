using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.Services.Kitsu
{

    public static class KitsuApi
    {

        private static string BaseEndpoint => "https://kitsu.io/api/edge";

        private static HttpClient HttpClient { get; } = new HttpClient();

        #region Anime APIs

        private static string SearchAnimeEndpoint => BaseEndpoint + "/anime?filter[text]={0}&page[limit]={1}&page[offset]={2}";
        private static string GetTrendingAnimeEndpoint => BaseEndpoint + "/trending/anime";
        private static string GetAnimeEndpoint => BaseEndpoint + "/anime/{0}";
        private static string GetAnimeGenresEndpoint => BaseEndpoint + "/anime/{0}/genres";

        public static async Task<KitsuData<KitsuAnimeAttributes>[]> SearchAnimeAsync(string query, int pageIndex = 1, int receiveCount = 20)
        {
            try
            {
                var pageCount = 0;
                if (pageIndex > 1)
                    pageCount = receiveCount * pageIndex;
                var httpResponse = await HttpClient.GetAsync(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query), receiveCount, pageCount));
                httpResponse.EnsureSuccessStatusCode();
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<KitsuResponses<KitsuAnimeAttributes>>(responseContent).Data;
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public static async Task<KitsuData<KitsuAnimeAttributes>[]> GetTrendingAnimeAsync()
        {
            try
            {
                var httpResponse = await HttpClient.GetAsync(GetTrendingAnimeEndpoint);
                httpResponse.EnsureSuccessStatusCode();
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<KitsuResponses<KitsuAnimeAttributes>>(responseContent).Data;
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public static async Task<KitsuData<KitsuAnimeAttributes>> GetAnimeAsync(string id)
        {
            try
            {
                var httpResponse = await HttpClient.GetAsync(string.Format(GetAnimeEndpoint, id));
                httpResponse.EnsureSuccessStatusCode();
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<KitsuResponse<KitsuAnimeAttributes>>(responseContent).Data;
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public static async Task<KitsuData<KitsuGenreAttributes>[]> GetAnimeGenresAsync(string id)
        {
            try
            {
                var httpResponse = await HttpClient.GetAsync(string.Format(GetAnimeGenresEndpoint, id));
                httpResponse.EnsureSuccessStatusCode();
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<KitsuResponses<KitsuGenreAttributes>>(responseContent).Data;
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        #endregion

        #region Manga APIs

        private static string SearchMangaEndpoint => BaseEndpoint + "/manga?filter[text]={0}&page[limit]={1}&page[offset]={2}";
        private static string GetTrendingMangaEndpoint => BaseEndpoint + "/trending/manga";
        private static string GetMangaEndpoint => BaseEndpoint + "/manga/{0}";
        private static string GetMangaGenresEndpoint => BaseEndpoint + "/manga/{0}/genres";

        public static async Task<KitsuData<KitsuMangaAttributes>[]> SearchMangaAsync(string query, int pageIndex = 1, int receiveCount = 20)
        {
            try
            {
                var pageCount = 0;
                if (pageIndex > 1)
                    pageCount = receiveCount * pageIndex;
                var httpResponse = await HttpClient.GetAsync(string.Format(SearchMangaEndpoint, Uri.EscapeDataString(query), receiveCount, pageCount));
                httpResponse.EnsureSuccessStatusCode();
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<KitsuResponses<KitsuMangaAttributes>>(responseContent).Data;
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public static async Task<KitsuData<KitsuMangaAttributes>[]> GetTrendingMangaAsync()
        {
            try
            {
                var httpResponse = await HttpClient.GetAsync(GetTrendingMangaEndpoint);
                httpResponse.EnsureSuccessStatusCode();
                var content = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<KitsuResponses<KitsuMangaAttributes>>(content).Data;
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public static async Task<KitsuData<KitsuMangaAttributes>> GetMangaAsync(string id)
        {
            try
            {
                var httpResponse = await HttpClient.GetAsync(string.Format(GetMangaEndpoint, id));
                httpResponse.EnsureSuccessStatusCode();
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<KitsuResponse<KitsuMangaAttributes>>(responseContent).Data;
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public static async Task<KitsuData<KitsuGenreAttributes>[]> GetMangaGenresAsync(string id)
        {
            try
            {
                var httpResponse = await HttpClient.GetAsync(string.Format(GetMangaGenresEndpoint, id));
                httpResponse.EnsureSuccessStatusCode();
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<KitsuResponses<KitsuGenreAttributes>>(responseContent).Data;
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        #endregion

    }

}
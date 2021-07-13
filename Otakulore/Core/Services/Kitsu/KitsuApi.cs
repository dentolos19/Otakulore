using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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
            var pageCount = 0;
            if (pageIndex > 1)
                pageCount = receiveCount * pageIndex;
            var httpResponse = await HttpClient.GetAsync(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query), receiveCount, pageCount));
            if (!httpResponse.IsSuccessStatusCode)
                return null;
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponses<KitsuAnimeAttributes>>(responseContent).Data;
        }

        public static async Task<KitsuData<KitsuAnimeAttributes>[]> GetTrendingAnimeAsync()
        {
            var httpResponse = await HttpClient.GetAsync(GetTrendingAnimeEndpoint);
            if (!httpResponse.IsSuccessStatusCode)
                return null;
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponses<KitsuAnimeAttributes>>(responseContent).Data;
        }

        public static async Task<KitsuData<KitsuAnimeAttributes>> GetAnimeAsync(string id)
        {
            var httpResponse = await HttpClient.GetAsync(string.Format(GetAnimeEndpoint, id));
            if (!httpResponse.IsSuccessStatusCode)
                return null;
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponse<KitsuAnimeAttributes>>(responseContent).Data;
        }

        public static async Task<KitsuData<KitsuGenreAttributes>[]> GetAnimeGenresAsync(string id)
        {
            var httpResponse = await HttpClient.GetAsync(string.Format(GetAnimeGenresEndpoint, id));
            if (!httpResponse.IsSuccessStatusCode)
                return null;
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponses<KitsuGenreAttributes>>(responseContent).Data;
        }

        #endregion

        #region Manga APIs

        private static string SearchMangaEndpoint => BaseEndpoint + "/manga?filter[text]={0}&page[limit]={1}&page[offset]={2}";
        private static string GetTrendingMangaEndpoint => BaseEndpoint + "/trending/manga";
        private static string GetMangaEndpoint => BaseEndpoint + "/manga/{0}";

        public static async Task<KitsuData<KitsuMangaAttributeses>[]> SearchMangaAsync(string query, int pageIndex = 1, int receiveCount = 20)
        {
            var pageCount = 0;
            if (pageIndex > 1)
                pageCount = receiveCount * pageIndex;
            var httpResponse = await HttpClient.GetAsync(string.Format(SearchMangaEndpoint, Uri.EscapeDataString(query), receiveCount, pageCount));
            if (!httpResponse.IsSuccessStatusCode)
                return null;
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponses<KitsuMangaAttributeses>>(responseContent).Data;
        }

        public static async Task<KitsuData<KitsuMangaAttributeses>[]> GetTrendingMangaAsync()
        {
            var httpResponse = await HttpClient.GetAsync(GetTrendingMangaEndpoint);
            if (!httpResponse.IsSuccessStatusCode)
                return null;
            var content = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponses<KitsuMangaAttributeses>>(content).Data;
        }

        public static async Task<KitsuData<KitsuMangaAttributeses>> GetMangaAsync(string id)
        {
            var httpResponse = await HttpClient.GetAsync(string.Format(GetMangaEndpoint, id));
            if (!httpResponse.IsSuccessStatusCode)
                return null;
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponse<KitsuMangaAttributeses>>(responseContent).Data;
        }

        #endregion

    }

}
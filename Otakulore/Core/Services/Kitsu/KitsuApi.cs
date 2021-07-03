using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otakulore.Core.Services.Kitsu
{

    public static class KitsuApi
    {

        private static string BaseEndpoint => "https://kitsu.io/api/edge";
        private static string SearchAnimeEndpoint => BaseEndpoint + "/anime?filter[text]={0}&page[limit]={1}&page[offset]={2}";
        private static string GetTrendingAnimeEndpoint => BaseEndpoint + "/trending/anime";
        private static string GetAnimeEndpoint => BaseEndpoint + "/anime/{0}";
        private static string GetAnimeGenresEndpoint => BaseEndpoint + "/anime/{0}/genres";
        private static string GetAnimeEpisodesEndpoint => BaseEndpoint + "/anime/{0}/episodes?page[limit]=20";

        private static HttpClient RestClient => new HttpClient();

        public static async Task<KitsuData<KitsuAnimeAttributes>[]> SearchAnimeAsync(string query, int pageIndex = 1, int receiveCount = 10)
        {
            var pageCount = 0;
            if (pageIndex > 1)
                pageCount = receiveCount * pageIndex;
            var response = await RestClient.GetAsync(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query), receiveCount, pageCount));
            if (!response.IsSuccessStatusCode)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponses<KitsuAnimeAttributes>>(content).Data;
        }

        public static async Task<KitsuData<KitsuAnimeAttributes>[]> GetTrendingAnimeAsync()
        {
            var response = await RestClient.GetAsync(GetTrendingAnimeEndpoint);
            if (!response.IsSuccessStatusCode)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponses<KitsuAnimeAttributes>>(content).Data;
        }

        public static async Task<KitsuData<KitsuAnimeAttributes>> GetAnimeAsync(string id)
        {
            var response = await RestClient.GetAsync(string.Format(GetAnimeEndpoint, id));
            if (!response.IsSuccessStatusCode)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponse<KitsuAnimeAttributes>>(content).Data;
        }

        public static async Task<KitsuData<KitsuGenreAttributes>[]> GetAnimeGenresAsync(string id)
        {
            var response = await RestClient.GetAsync(string.Format(GetAnimeGenresEndpoint, id));
            if (!response.IsSuccessStatusCode)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponses<KitsuGenreAttributes>>(content).Data;
        }

        public static async Task<KitsuData<KitsuEpisodeAttributes>[]> GetAnimeEpisodesAsync(string id)
        {
            var httpResponse = await RestClient.GetAsync(string.Format(GetAnimeEpisodesEndpoint, id));
            if (!httpResponse.IsSuccessStatusCode)
                return null;
            var responseData = JsonSerializer.Deserialize<KitsuResponses<KitsuEpisodeAttributes>>(await httpResponse.Content.ReadAsStringAsync());
            var episodeList = new List<KitsuData<KitsuEpisodeAttributes>>();
            AddEpisodes:
            episodeList.AddRange(responseData.Data);
            if (!string.IsNullOrEmpty(responseData.Links.NextPaginationUrl))
            {
                httpResponse = await RestClient.GetAsync(string.Format(responseData.Links.NextPaginationUrl));
                if (!httpResponse.IsSuccessStatusCode)
                    return episodeList.ToArray();
                responseData = JsonSerializer.Deserialize<KitsuResponses<KitsuEpisodeAttributes>>(await httpResponse.Content.ReadAsStringAsync());
                goto AddEpisodes;
            }
            return episodeList.ToArray();
        }

    }

}
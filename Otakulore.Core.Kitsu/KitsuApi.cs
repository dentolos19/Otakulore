using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otakulore.Core.Kitsu
{

    public static class KitsuApi
    {

        private static string BaseEndpoint => "https://kitsu.io/api/edge";
        private static string SearchAnimeEndpoint => BaseEndpoint + "/anime?filter[text]={0}&page[limit]={1}&page[offset]={2}";
        private static string GetAnimeEndpoint => BaseEndpoint + "/anime/{0}";
        private static string GetTrendingAnimeEndpoint => BaseEndpoint + "/trending/anime";

        private static HttpClient RestClient => new();

        public static async Task<KitsuData[]> SearchAnimeAsync(string query, int pageIndex = 1, int count = 10)
        {
            var page = 0;
            if (pageIndex > 1)
                page = count * pageIndex;
            var response = await RestClient.GetAsync(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query), count, page));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponses>(content)!.Data;
        }

        public static async Task<KitsuData> GetAnimeAsync(string id)
        {
            var response = await RestClient.GetAsync(string.Format(GetAnimeEndpoint, id));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponse>(content)!.Data;
        }

        public static async Task<KitsuData[]> GetTrendingAnimeAsync()
        {
            var response = await RestClient.GetAsync(GetTrendingAnimeEndpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponses>(content)!.Data;
        }

    }

}
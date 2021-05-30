using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otakulore.Core.Kitsu
{

    public static class KitsuApi
    {

        private static string BaseEndpoint => "https://kitsu.io/api/edge";
        private static string FilterAnimeEndpoint => BaseEndpoint + "/anime?filter[text]={0}";
        private static string GetAnimeEndpoint => BaseEndpoint + "/anime/{0}";

        private static HttpClient RestClient => new();

        public static async Task<KitsuData<KitsuAnimeAttributes>[]> SearchAnimeAsync(string query)
        {
            var response = await RestClient.GetAsync(string.Format(FilterAnimeEndpoint, Uri.EscapeDataString(query)));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponses<KitsuAnimeAttributes>>(content)!.Data;
        }

        public static async Task<KitsuData<KitsuAnimeAttributes>> GetAnimeAsync(string id)
        {
            var response = await RestClient.GetAsync(string.Format(GetAnimeEndpoint, id));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponse<KitsuAnimeAttributes>>(content)!.Data;
        }

    }

}
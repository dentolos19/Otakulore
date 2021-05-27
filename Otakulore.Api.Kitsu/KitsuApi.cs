using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otakulore.Api.Kitsu
{

    public static class KitsuApi
    {

        private static string BaseEndpoint => "https://kitsu.io/api/edge";
        private static HttpClient RestClient => new();

        public static async Task<KitsuData<KitsuAnimeAttributes>[]> FilterAnime(string query)
        {
            var response = await RestClient.GetAsync(BaseEndpoint + "/anime?filter[text]=" + Uri.EscapeDataString(query));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KitsuResponse<KitsuAnimeAttributes>>(content)!.Data;
        }

    }

}
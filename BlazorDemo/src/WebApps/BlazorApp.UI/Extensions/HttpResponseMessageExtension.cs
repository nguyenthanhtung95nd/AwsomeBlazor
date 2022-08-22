using Newtonsoft.Json;

namespace BlazorApp.UI.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<T> ReadAsObjectAsync<T>(this HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
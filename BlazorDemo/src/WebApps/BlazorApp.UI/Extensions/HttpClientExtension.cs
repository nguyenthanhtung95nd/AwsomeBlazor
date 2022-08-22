using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace BlazorApp.UI.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<HttpResponseMessage> PostJsonContentAsync(this HttpClient client, string url, object obj)
        {
            HttpContent content = CreateJsonContent(obj);
            return await client.PostAsync(url, content);
        }

        public static async Task<HttpResponseMessage> PutJsonContentAsync(this HttpClient client, string url, object obj)
        {
            HttpContent content = CreateJsonContent(obj);
            return await client.PutAsync(url, content);
        }

        public static async Task<HttpResponseMessage> PostByteArrayContentAsync(this HttpClient client, string url, object obj, string contentType = "application/x-www-form-urlencoded")
        {
            HttpContent httpContent = CreateByteArrayContent(obj, contentType);
            client.DefaultRequestHeaders.TryAddWithoutValidation(HeaderNames.Accept, contentType);
            return await client.PostAsync(url, httpContent);
        }

        public static async Task<HttpResponseMessage> PutByteArrayContentAsync(this HttpClient client, string url, object obj, string contentType = "application/x-www-form-urlencoded")
        {
            HttpContent httpContent = CreateByteArrayContent(obj, contentType);
            client.DefaultRequestHeaders.TryAddWithoutValidation(HeaderNames.Accept, contentType);
            return await client.PutAsync(url, httpContent);
        }

        private static HttpContent CreateJsonContent(object obj)
        {
            HttpContent content;
            if (obj != null)
            {
                var postJsonData = JsonConvert.SerializeObject(obj);
                content = new StringContent(postJsonData, new UTF8Encoding());
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            else
            {
                content = new StringContent(string.Empty);
            }
            return content;
        }

        private static HttpContent CreateByteArrayContent(object obj, string contentType)
        {
            if (obj != null)
            {
                var jsonSerializerSettings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };
                var postJson = JsonConvert.SerializeObject(obj, jsonSerializerSettings);
                byte[] postData = Encoding.UTF8.GetBytes(postJson);
                var httpContent = new ByteArrayContent(postData);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                httpContent.Headers.ContentLength = postData.Length;
                return httpContent;
            }
            return new StringContent(string.Empty);
        }
    }
}

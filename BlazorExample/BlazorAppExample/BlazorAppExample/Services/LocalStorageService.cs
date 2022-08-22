using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorAppExample.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime js;
        public LocalStorageService(IJSRuntime JsRuntime)
        {
            js = JsRuntime;
        }

        public async Task<T> GetItemAsync<T>(string key)
        {
            var json = await js.InvokeAsync<string>("exJsInterop.getLocalStorage", key);

            return string.IsNullOrEmpty(json)
                    ? default
                    : JsonSerializer.Deserialize<T>(json);
        }

        public async Task SetItemAsync<T>(string key, T item)
        {
            await js.InvokeVoidAsync("exJsInterop.setLocalStorage", key, JsonSerializer.Serialize(item));
        }
    }
}

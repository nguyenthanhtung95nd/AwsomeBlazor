using BlazorApp.Models;
using System.Net.Http.Json;

namespace BlazorApp.Client.Services
{
    public class UserApiClient : IUserApiClient
    {
        public readonly HttpClient _httpClient;

        public UserApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AssigneeDto>> GetAssignees()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AssigneeDto>>($"/api/users");
            return result;
        }
    }
}

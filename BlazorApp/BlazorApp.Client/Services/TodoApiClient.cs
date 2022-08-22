using BlazorApp.Models;
using BlazorApp.Models.SeedWork;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace BlazorApp.Client.Services
{
    public class TodoApiClient : ITodoApiClient
    {
        private readonly HttpClient _httpClient;

        public TodoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedList<TodoDto>> GetTodoList(TodoListSearch todoListSearch)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = todoListSearch.PageNumber.ToString()
            };

            if (!string.IsNullOrEmpty(todoListSearch.Name))
                queryStringParam.Add("name", todoListSearch.Name);
            if (todoListSearch.AssigneeId.HasValue)
                queryStringParam.Add("assigneeId", todoListSearch.AssigneeId.ToString());
            if (todoListSearch.Priority.HasValue)
                queryStringParam.Add("priority", todoListSearch.Priority.ToString());

            string url = QueryHelpers.AddQueryString("/api/Todos", queryStringParam);

            var result = await _httpClient.GetFromJsonAsync<PagedList<TodoDto>>(url);
            return result;
        }

        public async Task<TodoDto> GetTodoDetail(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<TodoDto>($"/api/Todos/{id}");
            return result;
        }

        public async Task<bool> CreateTodo(TodoCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Todos", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTodo(int id, TodoUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Todos/{id}", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTodo(int id)
        {
            var result = await _httpClient.DeleteAsync($"/api/Todos/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> AssignTodo(int id, AssignTodoRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Todos/{id}/assign", request);
            return result.IsSuccessStatusCode;
        }
    }
}

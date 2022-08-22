using BlazorApp.Models;
using BlazorApp.Models.SeedWork;

namespace BlazorApp.Client.Services
{
    public interface ITodoApiClient
    {
        Task<PagedList<TodoDto>> GetTodoList(TodoListSearch todoListSearch);

        Task<TodoDto> GetTodoDetail(int id);

        Task<bool> CreateTodo(TodoCreateRequest request);

        Task<bool> UpdateTodo(int id, TodoUpdateRequest request);

        Task<bool> DeleteTodo(int id);

        Task<bool> AssignTodo(int id, AssignTodoRequest request);
    }
}

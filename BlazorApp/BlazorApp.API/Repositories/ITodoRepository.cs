using BlazorApp.API.Entities;
using BlazorApp.Models;
using BlazorApp.Models.SeedWork;

namespace BlazorApp.API.Repositories
{
    public interface ITodoRepository
    {
        Task<PagedList<Todo>> GetTodoListByUserId(Guid userId, TodoListSearch todoListSearch);

        Task<PagedList<Todo>> GetTodoList(TodoListSearch todoListSearch);

        Task<Todo> Create(Todo todo);

        Task<Todo> Update(Todo todo);

        Task<Todo> Delete(Todo todo);

        Task<Todo> GetById(int id);
    }
}

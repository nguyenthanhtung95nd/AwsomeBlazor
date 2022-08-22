using BlazorApp.API.Data;
using BlazorApp.API.Entities;
using BlazorApp.Models;
using BlazorApp.Models.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.API.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoListDbContext _context;

        public TodoRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Todo>> GetTodoListByUserId(Guid userId, TodoListSearch todoListSearch)
        {
            var query = _context.Todos
                    .Where(x => x.AssigneeId == userId)
                 .Include(x => x.Assignee).AsQueryable();

            if (!string.IsNullOrEmpty(todoListSearch.Name))
                query = query.Where(x => x.Name.Contains(todoListSearch.Name));

            if (todoListSearch.AssigneeId.HasValue)
                query = query.Where(x => x.AssigneeId == todoListSearch.AssigneeId.Value);

            if (todoListSearch.Priority.HasValue)
                query = query.Where(x => x.Priority == todoListSearch.Priority.Value);

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(x => x.CreatedDate)
                .Skip((todoListSearch.PageNumber - 1) * todoListSearch.PageSize)
                .Take(todoListSearch.PageSize)
                .ToListAsync();
            return new PagedList<Todo>(data, count, todoListSearch.PageNumber, todoListSearch.PageSize);
        }

        public async Task<PagedList<Todo>> GetTodoList(TodoListSearch todoListSearch)
        {
            var query = _context.Todos
                .Include(x => x.Assignee).AsQueryable();

            if (!string.IsNullOrEmpty(todoListSearch.Name))
                query = query.Where(x => x.Name.Contains(todoListSearch.Name));

            if (todoListSearch.AssigneeId.HasValue)
                query = query.Where(x => x.AssigneeId == todoListSearch.AssigneeId.Value);

            if (todoListSearch.Priority.HasValue)
                query = query.Where(x => x.Priority == todoListSearch.Priority.Value);

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(x => x.CreatedDate)
                .Skip((todoListSearch.PageNumber - 1) * todoListSearch.PageSize)
                .Take(todoListSearch.PageSize)
                .ToListAsync();
            return new PagedList<Todo>(data, count, todoListSearch.PageNumber, todoListSearch.PageSize);

        }

        public async Task<Todo> Create(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> Delete(Todo todo)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> GetById(int id)
        {
            return await _context.Todos.FindAsync(id) ?? new Todo();
        }

        public async Task<Todo> Update(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
            return todo;
        }
    }
}

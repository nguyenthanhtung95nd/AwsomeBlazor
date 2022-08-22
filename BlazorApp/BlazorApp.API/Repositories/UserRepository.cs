using BlazorApp.API.Data;
using BlazorApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoListDbContext _context;

        public UserRepository(TodoListDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }
    }
}

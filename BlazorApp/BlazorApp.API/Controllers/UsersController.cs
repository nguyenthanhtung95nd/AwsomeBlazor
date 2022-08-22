using BlazorApp.API.Repositories;
using BlazorApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetUserList();
            var assignees = users.Select(x => new AssigneeDto()
            {
                Id = x.Id,
                FullName = x.FirstName + " " + x.LastName
            });

            return Ok(assignees);
        }
    }
}
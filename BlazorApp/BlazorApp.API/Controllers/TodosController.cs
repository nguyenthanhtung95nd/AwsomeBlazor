using BlazorApp.API.Extensions;
using BlazorApp.API.Repositories;
using BlazorApp.Models;
using BlazorApp.Models.Enums;
using BlazorApp.Models.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodosController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TodoListSearch todoListSearch)
        {
            var pagedList = await _todoRepository.GetTodoList(todoListSearch);
            var todoDtos = pagedList.Items.Select(x => new TodoDto()
            {
                Status = x.Status,
                Name = x.Name,
                AssigneeId = x.AssigneeId,
                CreatedDate = x.CreatedDate ?? DateTime.Now,
                Priority = x.Priority,
                Id = x.Id,
                AssigneeName = x.Assignee != null ? x.Assignee.FirstName + ' ' + x.Assignee.LastName : "N/A"
            });

            return Ok(
                    new PagedList<TodoDto>(todoDtos.ToList(),
                        pagedList.MetaData.TotalCount,
                        pagedList.MetaData.CurrentPage,
                        pagedList.MetaData.PageSize)
                );
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetByAssigneeId([FromQuery] TodoListSearch todoListSearch)
        {
            var userId = User.GetUserId();
            var pagedList = await _todoRepository.GetTodoListByUserId(Guid.Parse(userId), todoListSearch);
            var TodoDtos = pagedList.Items.Select(x => new TodoDto()
            {
                Status = x.Status,
                Name = x.Name,
                AssigneeId = x.AssigneeId,
                CreatedDate = x.CreatedDate ?? DateTime.Now,
                Priority = x.Priority,
                Id = x.Id,
                AssigneeName = x.Assignee != null ? x.Assignee.FirstName + ' ' + x.Assignee.LastName : "N/A"
            });

            return Ok(
                    new PagedList<TodoDto>(TodoDtos.ToList(),
                        pagedList.MetaData.TotalCount,
                        pagedList.MetaData.CurrentPage,
                        pagedList.MetaData.PageSize)
                );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todo = await _todoRepository.Create(new Entities.Todo()
            {
                Name = request.Name ?? "",
                Priority = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
                Status = Status.Open,
                CreatedDate = DateTime.Now,
                Id = request.Id
            });
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todoFromDb = await _todoRepository.GetById(id);

            if (todoFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            todoFromDb.Name = request.Name ?? "";
            todoFromDb.Priority = request.Priority;

            var taskResult = await _todoRepository.Update(todoFromDb);

            return Ok(new TodoDto()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Id,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate ?? DateTime.Now,
            });
        }

        [HttpPut]
        [Route("{id}/assign")]
        public async Task<IActionResult> AssignTask(int id, [FromBody] AssignTodoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todoFromDb = await _todoRepository.GetById(id);

            if (todoFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            todoFromDb.AssigneeId = request.UserId.Value == Guid.Empty ? null : request.UserId.Value;

            var taskResult = await _todoRepository.Update(todoFromDb);

            return Ok(new TodoDto()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Id,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate ?? DateTime.Now
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var todo = await _todoRepository.GetById(id);
            if (todo == null) return NotFound($"{id} is not found");
            return Ok(new TodoDto()
            {
                Name = todo.Name,
                Status = todo.Status,
                Id = todo.Id,
                AssigneeId = todo.AssigneeId,
                Priority = todo.Priority,
                CreatedDate = todo.CreatedDate ?? DateTime.Now
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var todo = await _todoRepository.GetById(id);
            if (todo == null) return NotFound($"{id} is not found");

            await _todoRepository.Delete(todo);
            return Ok(new TodoDto()
            {
                Name = todo.Name,
                Status = todo.Status,
                Id = todo.Id,
                AssigneeId = todo.AssigneeId,
                Priority = todo.Priority,
                CreatedDate = todo.CreatedDate ?? DateTime.Now
            });
        }
    }
}
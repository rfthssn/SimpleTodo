using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleTodo.Api.Models;
using SimpleTodo.Api.Repositories;

namespace SimpleTodo.Api.Controllers
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
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos(bool? isCompleted)
        {
            var todos = await _todoRepository.GetTodosAsync(isCompleted);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoById(int id)
        {
            var todo = await _todoRepository.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound("Todo Not Found");
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> AddTodo(TodoItem todo)
        {
            var newTodo = await _todoRepository.AddTodoAsync(todo);
            return Ok(newTodo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoItem todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
            await _todoRepository.UpdateTodoAsync(todo);

            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _todoRepository.DeleteTodoAsync(id);
            return Ok();
        }
    }
}

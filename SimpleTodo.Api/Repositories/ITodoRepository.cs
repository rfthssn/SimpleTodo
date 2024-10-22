using SimpleTodo.Api.Models;

namespace SimpleTodo.Api.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetTodosAsync(bool? isCompleted);
        Task<TodoItem> GetTodoByIdAsync(int id);
        Task<TodoItem> AddTodoAsync(TodoItem todo);
        Task UpdateTodoAsync(TodoItem todo);
        Task DeleteTodoAsync(int id);

    }
}

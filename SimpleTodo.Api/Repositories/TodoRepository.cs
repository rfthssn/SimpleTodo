using Microsoft.EntityFrameworkCore;
using SimpleTodo.Api.Data;
using SimpleTodo.Api.Models;

namespace SimpleTodo.Api.Repositories
{

    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetTodosAsync(bool? isCompleted)
        {
            if (isCompleted.HasValue)
            {
                return await _context.Todos.Where(t => t.IsCompleted == isCompleted.Value).ToListAsync();
            }
            return await _context.Todos.ToListAsync();
        }

        public async Task<TodoItem> GetTodoByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<TodoItem> AddTodoAsync(TodoItem todo)
        {
            try
            {
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
                return todo;
            }
            catch(Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the todo item.", ex);
            }

        }

        public async Task UpdateTodoAsync(TodoItem todo)
        {
            try
            {
                _context.Entry(todo).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the todo item.", ex);
            }

        }

        public async Task DeleteTodoAsync(int id)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo != null)
                {
                    _context.Todos.Remove(todo);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the todo item.", ex);
            }

        }
    }
}

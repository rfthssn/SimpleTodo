using SimpleTodo.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTodo.Maui.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetTodosAsync(bool? isCompleted = null);
        Task AddTodoAsync(TodoItem todo);
        Task UpdateTodoAsync(TodoItem todo);
        Task DeleteTodoAsync(int id);
    }
}

using RestSharp;
using SimpleTodo.Maui.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleTodo.Maui.Services
{
    public class TodoService : ITodoService
    {
        private readonly RestClient _restClient;

        public TodoService()
        {
            _restClient = new RestClient("http://10.0.2.2:5278/");
        }

        public async Task<List<TodoItem>> GetTodosAsync(bool? isCompleted = null)
        {
            var url = isCompleted != null ? "api/todos?isCompleted="+isCompleted : "api/todos";
            var request = new RestRequest(url, Method.Get);

            var response = await _restClient.ExecuteAsync<List<TodoItem>>(request);

            return response.Data ?? new List<TodoItem>();
        }

        public async Task AddTodoAsync(TodoItem todo)
        {
            var request = new RestRequest("api/todos", Method.Post);
            request.AddJsonBody(todo);

            var response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error adding todo: {response.ErrorMessage}");
            }
        }

        public async Task UpdateTodoAsync(TodoItem todo)
        {
            var request = new RestRequest($"api/todos/{todo.Id}", Method.Put);
            request.AddJsonBody(todo);

            var response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error updating todo: {response.ErrorMessage}");
            }
        }

        public async Task DeleteTodoAsync(int id)
        {
            var request = new RestRequest($"api/todos/{id}", Method.Delete);

            var response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error deleting todo: {response.ErrorMessage}");
            }
        }
    }
}

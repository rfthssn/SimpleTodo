using Microsoft.EntityFrameworkCore;
using SimpleTodo.Api.Data;
using SimpleTodo.Api.Models;
using SimpleTodo.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTodo.Api.Tests.Repositories
{

    public class TodoRepositoryTests
    {
        private readonly TodoRepository _repository;
        private readonly ApplicationDbContext _context;

        public TodoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SimpleTodoTest")
                .Options;

            _context = new ApplicationDbContext(options);

            SeedDatabase();

            _repository = new TodoRepository(_context);
        }

        private void SeedDatabase()
        {
            _context.Todos.RemoveRange(_context.Todos);
            _context.SaveChanges();

            _context.Todos.AddRange(new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Test Todo 1", Description = "Test Description 1",  IsCompleted = true },
                new TodoItem {Id = 2, Title = "Test Todo 2", Description = "Test Description 2", IsCompleted = false }
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetTodosAsync_ShouldReturnAllTodos()
        {
            // Act
            var todos = await _repository.GetTodosAsync(null);

            // Assert
            Assert.NotNull(todos);
            var todoList = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(todos);
            Assert.Equal(2, ((List<TodoItem>)todoList).Count);
        }

        [Fact]
        public async Task GetTodosAsync_ShouldReturnCompletedTodos()
        {
            // Act
            var todos = await _repository.GetTodosAsync(true);

            // Assert
            Assert.NotNull(todos);
            var todoList = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(todos);
            Assert.Single(todoList);
            Assert.True(((List<TodoItem>)todoList)[0].IsCompleted);
        }

        [Fact]
        public async Task GetTodosAsync_ShouldReturnInCompletedTodos()
        {
            // Act
            var todos = await _repository.GetTodosAsync(false);

            // Assert
            Assert.NotNull(todos);
            var todoList = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(todos);
            Assert.Single(todoList);
            Assert.True(!((List<TodoItem>)todoList)[0].IsCompleted);
        }

        [Fact]
        public async Task GetTodoByIdAsync_ShouldReturnCorrectTodo()
        {
            // Act
            var todo = await _repository.GetTodoByIdAsync(1);

            // Assert
            Assert.NotNull(todo);
            Assert.Equal(1, todo.Id);
            Assert.Equal("Test Todo 1", todo.Title);
        }

        [Fact]
        public async Task AddTodoAsync_ShouldAddNewTodo()
        {
            // Arrange
            var newTodo = new TodoItem { Title = "New Todo", Description = "New Todo Description", IsCompleted = false };

            // Act
            var addedTodo = await _repository.AddTodoAsync(newTodo);

            // Assert
            var savedTodo = await _context.Todos.FindAsync(addedTodo.Id);
            Assert.NotNull(savedTodo);
            Assert.Equal("New Todo", savedTodo.Title);
        }

        [Fact]
        public async Task UpdateTodoAsync_ShouldUpdateExistingTodo()
        {
            // Arrange
            var todoToUpdate = await _repository.GetTodoByIdAsync(1);
            todoToUpdate.Title = "Updated Todo";

            // Act
            await _repository.UpdateTodoAsync(todoToUpdate);

            // Assert
            var updatedTodo = await _repository.GetTodoByIdAsync(1);
            Assert.Equal("Updated Todo", updatedTodo.Title);
        }

        [Fact]
        public async Task DeleteTodoAsync_ShouldRemoveTodo()
        {
            // Act
            await _repository.DeleteTodoAsync(1);

            // Assert
            var deletedTodo = await _repository.GetTodoByIdAsync(1);
            Assert.Null(deletedTodo);
        }
    }
}

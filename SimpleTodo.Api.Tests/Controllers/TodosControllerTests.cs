using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleTodo.Api.Controllers;
using SimpleTodo.Api.Models;
using SimpleTodo.Api.Repositories;
using SimpleTodo.Api.Tests.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SimpleTodo.Api.Tests
{
    public class TodosControllerTests
    {
        private readonly Mock<ITodoRepository> _mockRepo;
        private readonly TodosController _controller;

        public TodosControllerTests()
        {
            _mockRepo = new Mock<ITodoRepository>();
            _controller = new TodosController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetTodos_ShouldReturnOkResult_WithAllTodos()
        {
            // Arrange
            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Test Todo 1", Description = "Test Description 1",  IsCompleted = true },
                new TodoItem {Id = 2, Title = "Test Todo 2", Description = "Test Description 2", IsCompleted = false }
            };
            _mockRepo.Setup(repo => repo.GetTodosAsync(null)).ReturnsAsync(todos);

            // Act
            var result = await _controller.GetTodos(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<TodoItem>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetTodoById_ShouldReturnNotFound_WhenTodoDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetTodoByIdAsync(1)).ReturnsAsync((TodoItem)null);

            // Act
            var result = await _controller.GetTodoById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetTodoById_ShouldReturnOk_WhenTodoExists()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo 1", Description = "Test Description 1", IsCompleted = true };
            _mockRepo.Setup(repo => repo.GetTodoByIdAsync(1)).ReturnsAsync(todo);

            // Act
            var result = await _controller.GetTodoById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TodoItem>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task AddTodo_ShouldReturnOkResult_WhenTodoIsAdded()
        {
            // Arrange
            var newTodo = new TodoItem { Title = "New Todo", Description = "New Todo Description", IsCompleted = false };
            _mockRepo.Setup(repo => repo.AddTodoAsync(newTodo)).ReturnsAsync(newTodo);

            // Act
            var result = await _controller.AddTodo(newTodo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TodoItem>(okResult.Value);
            Assert.Equal("New Todo", returnValue.Title);
        }

        [Fact]
        public async Task UpdateTodo_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo", Description = "New Todo Description", IsCompleted = false };

            // Act
            var result = await _controller.UpdateTodo(2, todo);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateTodo_ShouldReturnOkResult_WhenTodoIsUpdated()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Updated Todo", Description = "Updated Todo Description", IsCompleted = false };
            _mockRepo.Setup(repo => repo.UpdateTodoAsync(todo)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateTodo(1, todo);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteTodo_ShouldReturnOkResult_WhenTodoIsDeleted()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteTodoAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteTodo(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}

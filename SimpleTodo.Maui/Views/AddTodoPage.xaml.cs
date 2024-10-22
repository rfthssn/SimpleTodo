using SimpleTodo.Maui.Models;
using SimpleTodo.Maui.ViewModels;

namespace SimpleTodo.Maui.Views;

public partial class AddTodoPage : ContentPage
{
    private readonly TodoViewModel _viewModel;

    public AddTodoPage(TodoViewModel viewModel)
    {
        InitializeComponent();

        // Assign the injected ViewModel to the _viewModel field
        _viewModel = viewModel;

        // Set the BindingContext to the ViewModel
        BindingContext = _viewModel;

        // Initialize a new todo item
        _viewModel.CurrentTodo = new TodoItem();
    }
}

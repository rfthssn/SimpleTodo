using SimpleTodo.Maui.Models;
using SimpleTodo.Maui.ViewModels;

namespace SimpleTodo.Maui.Views;

[QueryProperty(nameof(TodoItem), "TodoItem")]
public partial class EditTodoPage : ContentPage
{
    private readonly TodoViewModel _viewModel;

    public EditTodoPage(TodoItem todoItem, TodoViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;

        _viewModel.CurrentTodo = todoItem;
        BindingContext = _viewModel;
    }
}

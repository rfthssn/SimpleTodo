using SimpleTodo.Maui.Models;
using SimpleTodo.Maui.ViewModels;

namespace SimpleTodo.Maui.Views;

public partial class TodoPage : ContentPage
{
    private readonly TodoViewModel _viewModel;

    public TodoPage(TodoViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is TodoViewModel viewModel)
        {
           await viewModel.LoadTodosAsync();
        }
    }


    private async void OnAddTodoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddTodoPage(_viewModel));
    }

    private async void OnEditTodoClicked(object sender, EventArgs e)
    {
        var todoItem = (TodoItem)((ImageButton)sender).CommandParameter;

        if (todoItem != null)
        {
            await Navigation.PushAsync(new EditTodoPage(todoItem, _viewModel));
        }
    }


    private async void OnToggleCompletionClicked(object sender, EventArgs e)
    {
        var todoItem = (TodoItem)((Button)sender).CommandParameter;

        var viewModel = (TodoViewModel)this.BindingContext;
        viewModel.CurrentTodo = todoItem;

        viewModel.CurrentTodo.IsCompleted = !viewModel.CurrentTodo.IsCompleted;

        await viewModel.EditTodoAsync();
    }


}

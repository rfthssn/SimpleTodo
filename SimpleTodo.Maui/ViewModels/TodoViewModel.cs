using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleTodo.Maui.Models;
using SimpleTodo.Maui.Services;
using SimpleTodo.Maui.Views;
using System.Collections.ObjectModel;

namespace SimpleTodo.Maui.ViewModels
{
    public partial class TodoViewModel : ObservableObject
    {
        private readonly ITodoService _todoService;

        [ObservableProperty]
        private ObservableCollection<TodoItem> _todoItems;

        private ObservableCollection<TodoItem> _allTodoItems;

        [ObservableProperty]
        private TodoItem _currentTodo;

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private FilterOptions _selectedFilter;
        public TodoViewModel(ITodoService todoService)
        {
            _todoService = todoService;
            _todoItems = new ObservableCollection<TodoItem>();
            _allTodoItems = new ObservableCollection<TodoItem>();
            _selectedFilter = FilterOptions.All;
        }

        [RelayCommand]
        public async Task LoadTodosAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var todos = await _todoService.GetTodosAsync();
                TodoItems.Clear();
                _allTodoItems.Clear();
                foreach (var todo in todos)
                {
                    TodoItems.Add(todo);
                    _allTodoItems.Add(todo);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void ApplyFilter()
        {
            TodoItems.Clear();

            IEnumerable<TodoItem> filtered = _selectedFilter switch
            {
                FilterOptions.All => _allTodoItems, 
                FilterOptions.Completed => _allTodoItems.Where(t => t.IsCompleted), 
                FilterOptions.Incomplete => _allTodoItems.Where(t => !t.IsCompleted), 
                _ => _allTodoItems
            };

            foreach (var item in filtered)
            {
                TodoItems.Add(item);
            }
        }


        //public void OnSelectedFilterChanged(FilterOptions filter)
        //{
        //    SelectedFilter = filter;
        //    ApplyFilter(); 
        //}


        [RelayCommand]
        public async Task AddTodoAsync()
        {
            if (IsBusy) return;

            await _todoService.AddTodoAsync(CurrentTodo);
            TodoItems.Add(CurrentTodo);
            await Shell.Current.GoToAsync("..");
        }


        [RelayCommand]
        public async Task EditTodoAsync()
        {
            if (IsBusy) return;

            await _todoService.UpdateTodoAsync(CurrentTodo);
            await Shell.Current.GoToAsync("..");
        }


        [RelayCommand]
        public async Task DeleteTodoAsync(TodoItem todo)
        {
            if (IsBusy) return;

            await _todoService.DeleteTodoAsync(todo.Id);
            TodoItems.Remove(todo);
        }
    }
}

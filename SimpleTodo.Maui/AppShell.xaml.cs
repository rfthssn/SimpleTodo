using SimpleTodo.Maui.Views;

namespace SimpleTodo.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TodoPage), typeof(TodoPage));
            Routing.RegisterRoute(nameof(AddTodoPage), typeof(AddTodoPage));
            Routing.RegisterRoute(nameof(EditTodoPage), typeof(EditTodoPage));
        }
    }
}

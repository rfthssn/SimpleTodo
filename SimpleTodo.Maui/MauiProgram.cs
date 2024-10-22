using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SimpleTodo.Maui.Services;
using SimpleTodo.Maui.ViewModels;
using SimpleTodo.Maui.Views;

namespace SimpleTodo.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseMauiCommunityToolkit();


            builder.Services.AddTransient<ITodoService, TodoService>();

            builder.Services.AddTransient<TodoViewModel>();
            builder.Services.AddTransient<TodoPage>();      
            builder.Services.AddTransient<AddTodoPage>();   
            builder.Services.AddTransient<EditTodoPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

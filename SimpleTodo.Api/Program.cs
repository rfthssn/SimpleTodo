using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SimpleTodo.Api.Data.ApplicationDbContext>(opt =>
{
    try
    {
        var connectionString = builder.Configuration.GetConnectionString("SimpleTodoDatabase");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException("SimpleTodoDatabase", "Connection string 'SimpleTodoDatabase' is missing or empty.");
        }
        opt.UseInMemoryDatabase(connectionString);
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine($"Configuration Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while setting up the database: {ex.Message}");
        throw; 
    }
});

builder.Services.AddScoped<SimpleTodo.Api.Repositories.ITodoRepository, SimpleTodo.Api.Repositories.TodoRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

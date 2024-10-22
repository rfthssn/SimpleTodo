using Microsoft.EntityFrameworkCore;
using SimpleTodo.Api.Models;
using System.Collections.Generic;

namespace SimpleTodo.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<TodoItem> Todos { get; set; }
    }
}

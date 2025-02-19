using Microsoft.EntityFrameworkCore;
using TodoListModule.TodoItems;
using TodoListModule.EntityFrameworkCore.Configurations;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace TodoListModule.EntityFrameworkCore;

[ConnectionStringName(TodoListModuleDbProperties.ConnectionStringName)]
public class TodoListModuleDbContext : AbpDbContext<TodoListModuleDbContext>, ITodoListModuleDbContext
{

    public DbSet<TodoItem> TodoItems { get; set; }


    public TodoListModuleDbContext(DbContextOptions<TodoListModuleDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTodoListModule();
    }
}

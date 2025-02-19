using Microsoft.EntityFrameworkCore;
using TodoListModule.EntityFrameworkCore.Configurations;
using Volo.Abp;

namespace TodoListModule.EntityFrameworkCore;

public static class TodoListModuleDbContextModelCreatingExtensions
{
    public static void ConfigureTodoListModule(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
        builder.ApplyConfiguration(new TodoItemConfiguration());
    }
}

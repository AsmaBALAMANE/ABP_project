using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace TodoListModule.EntityFrameworkCore;

[ConnectionStringName(TodoListModuleDbProperties.ConnectionStringName)]
public interface ITodoListModuleDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}

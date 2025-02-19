using Microsoft.Extensions.DependencyInjection;
using TodoListModule.Repositories;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace TodoListModule.EntityFrameworkCore;

[DependsOn(
    typeof(TodoListModuleDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class TodoListModuleEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<TodoListModuleDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
            context.Services.AddTransient<ITodoItemRepository, TodoItemRepository>();
   
        });
    }
}

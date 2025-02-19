using Volo.Abp.Modularity;

namespace TodoListModule;

[DependsOn(
    typeof(TodoListModuleDomainModule),
    typeof(TodoListModuleTestBaseModule)
)]
public class TodoListModuleDomainTestModule : AbpModule
{

}

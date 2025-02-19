using Volo.Abp.Modularity;

namespace TodoListModule;

[DependsOn(
    typeof(TodoListModuleApplicationModule),
    typeof(TodoListModuleDomainTestModule)
    )]
public class TodoListModuleApplicationTestModule : AbpModule
{

}

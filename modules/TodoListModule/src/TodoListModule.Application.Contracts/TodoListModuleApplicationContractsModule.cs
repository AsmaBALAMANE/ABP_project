using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace TodoListModule;

[DependsOn(
    typeof(TodoListModuleDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class TodoListModuleApplicationContractsModule : AbpModule
{

}

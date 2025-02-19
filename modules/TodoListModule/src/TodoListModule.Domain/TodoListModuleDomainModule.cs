using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace TodoListModule;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(TodoListModuleDomainSharedModule)
)]
public class TodoListModuleDomainModule : AbpModule
{

}

using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace TodoListModule;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TodoListModuleHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class TodoListModuleConsoleApiClientModule : AbpModule
{

}

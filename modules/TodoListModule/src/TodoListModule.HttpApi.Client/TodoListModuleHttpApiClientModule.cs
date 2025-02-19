using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace TodoListModule;

[DependsOn(
    typeof(TodoListModuleApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class TodoListModuleHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(TodoListModuleApplicationContractsModule).Assembly,
            TodoListModuleRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TodoListModuleHttpApiClientModule>();
        });

    }
}

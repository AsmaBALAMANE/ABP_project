using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace TodoListModule;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class TodoListModuleInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TodoListModuleInstallerModule>();
        });
    }
}

using Localization.Resources.AbpUi;
using TodoListModule.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace TodoListModule;

[DependsOn(
    typeof(TodoListModuleApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class TodoListModuleHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TodoListModuleHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TodoListModuleResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}

using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.FluentValidation;

namespace TodoListModule;

[DependsOn(
    typeof(TodoListModuleDomainModule),
    typeof(TodoListModuleApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
[DependsOn(typeof(AbpFluentValidationModule))]
    public class TodoListModuleApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<TodoListModuleApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TodoListModuleApplicationModule>(validate: true);
        });
    }
}

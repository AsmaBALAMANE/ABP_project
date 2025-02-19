using Volo.Abp.Modularity;

namespace MainApp;

[DependsOn(
    typeof(MainAppDomainModule),
    typeof(MainAppTestBaseModule)
)]
public class MainAppDomainTestModule : AbpModule
{

}

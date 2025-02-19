using Volo.Abp.Modularity;

namespace MainApp;

public abstract class MainAppApplicationTestBase<TStartupModule> : MainAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

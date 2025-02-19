using Volo.Abp.Modularity;

namespace MainApp;

/* Inherit from this class for your domain layer tests. */
public abstract class MainAppDomainTestBase<TStartupModule> : MainAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

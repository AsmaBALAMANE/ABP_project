using Volo.Abp.Modularity;

namespace TodoListModule;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class TodoListModuleDomainTestBase<TStartupModule> : TodoListModuleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

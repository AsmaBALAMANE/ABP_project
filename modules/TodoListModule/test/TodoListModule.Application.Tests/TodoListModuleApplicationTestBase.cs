using Volo.Abp.Modularity;

namespace TodoListModule;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class TodoListModuleApplicationTestBase<TStartupModule> : TodoListModuleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

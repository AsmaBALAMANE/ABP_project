using MainApp.Localization;
using Volo.Abp.Application.Services;

namespace MainApp;

/* Inherit your application services from this class.
 */
public abstract class MainAppAppService : ApplicationService
{
    protected MainAppAppService()
    {
        LocalizationResource = typeof(MainAppResource);
    }
}

using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using MainApp.Localization;

namespace MainApp.Web;

[Dependency(ReplaceServices = true)]
public class MainAppBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MainAppResource> _localizer;

    public MainAppBrandingProvider(IStringLocalizer<MainAppResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}

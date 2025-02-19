using TodoListModule.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace TodoListModule.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class TodoListModulePageModel : AbpPageModel
{
    protected TodoListModulePageModel()
    {
        LocalizationResourceType = typeof(TodoListModuleResource);
        ObjectMapperContext = typeof(TodoListModuleWebModule);
    }
}

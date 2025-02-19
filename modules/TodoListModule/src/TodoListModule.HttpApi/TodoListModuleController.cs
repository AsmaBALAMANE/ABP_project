using TodoListModule.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TodoListModule;

public abstract class TodoListModuleController : AbpControllerBase
{
    protected TodoListModuleController()
    {
        LocalizationResource = typeof(TodoListModuleResource);
    }
}

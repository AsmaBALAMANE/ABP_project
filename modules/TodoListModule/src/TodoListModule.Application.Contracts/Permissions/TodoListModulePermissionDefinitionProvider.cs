using TodoListModule.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TodoListModule.Permissions;

public class TodoListModulePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TodoListModulePermissions.GroupName, L("Permission:TodoListModule"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TodoListModuleResource>(name);
    }
}

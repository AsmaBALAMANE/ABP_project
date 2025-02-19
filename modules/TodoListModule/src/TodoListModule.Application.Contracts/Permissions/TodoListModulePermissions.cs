using Volo.Abp.Reflection;

namespace TodoListModule.Permissions;

public class TodoListModulePermissions
{
    public const string GroupName = "TodoListModule";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TodoListModulePermissions));
    }
}

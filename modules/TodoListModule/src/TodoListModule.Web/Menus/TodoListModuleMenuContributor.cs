using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace TodoListModule.Web.Menus;

public class TodoListModuleMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(TodoListModuleMenus.Prefix, displayName: "TodoListModule", "~/TodoListModule", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}

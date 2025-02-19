namespace TodoListModule;

public static class TodoListModuleDbProperties
{
    public static string DbTablePrefix { get; set; } = "TodoListModule";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "TodoListModule";
}

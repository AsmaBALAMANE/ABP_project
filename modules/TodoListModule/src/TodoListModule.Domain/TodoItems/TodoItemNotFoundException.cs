using System;
using Volo.Abp;

namespace TodoListModule.TodoItems
{
    public class TodoItemNotFoundException : BusinessException
    {
        public TodoItemNotFoundException(Guid id) : base(TodoListModuleErrorCodes.TODO_ITEM_NOT_FOUND, $"TodoItem with id {id} was not found")
        {
            WithData("id", id);
        }
    }
}

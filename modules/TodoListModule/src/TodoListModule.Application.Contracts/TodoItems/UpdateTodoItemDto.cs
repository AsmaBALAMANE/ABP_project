using System;
using Volo.Abp.Application.Dtos;

namespace TodoListModule.TodoItems
{
    public class UpdateTodoItemDto 
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}

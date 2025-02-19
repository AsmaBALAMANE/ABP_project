using System;
using Volo.Abp.Application.Dtos;

namespace TodoListModule.TodoItems
{
    public class TodoItemDto : AuditedEntityDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}

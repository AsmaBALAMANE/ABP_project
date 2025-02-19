using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace TodoListModule.TodoItems
{
    public class TodoItem : AuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }

}

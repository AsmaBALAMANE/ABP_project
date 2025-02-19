using System;
using Volo.Abp.Application.Dtos;
namespace TodoListModule.TodoItems
{
    public class GetTodoItemListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public bool? IsCompleted { get; set; }
    }
}

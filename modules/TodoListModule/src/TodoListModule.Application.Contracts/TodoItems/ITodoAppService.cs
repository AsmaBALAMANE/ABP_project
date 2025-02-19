using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TodoListModule.TodoItems
{
    public interface ITodoAppService
    {
        Task<TodoItemDto> GetAsync(Guid id);
        Task<PagedResultDto<TodoItemDto>> GetFiltredListAsync(GetTodoItemListDto input);
        Task<TodoItemDto> CreateAsync(CreateTodoItemDto input);
        Task<TodoItemDto> UpdateAsync(Guid id, UpdateTodoItemDto input);
        Task<bool> DeleteAsync(Guid id);
        Task<List<TodoItemDto>?> GetListAsync();

    }
}

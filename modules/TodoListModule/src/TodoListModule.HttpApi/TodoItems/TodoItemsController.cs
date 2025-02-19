using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace TodoListModule.TodoItems;

[Area(TodoListModuleRemoteServiceConsts.ModuleName)]
[RemoteService(Name = TodoListModuleRemoteServiceConsts.RemoteServiceName)]
[Route("api/todo")]
[Authorize]
public class TodoItemsController : TodoListModuleController, ITodoAppService
{
    private readonly ITodoAppService _sampleAppService;

    public TodoItemsController(ITodoAppService sampleAppService)
    {
        _sampleAppService = sampleAppService;
    }

    /// <summary>
    /// Get TodoItem by Id
    /// </summary>
    /// <returns>TodoItemDto</returns>
    [HttpGet("{id:guid}")]
    public async Task<TodoItemDto> GetAsync(Guid id)
    {
        return await _sampleAppService.GetAsync(id);
    }
    /// <summary>
    /// Get list of TodoItems using filter on the TodoItem.Title and if the TodoItem is completed
    /// </summary>
    /// <returns>TodoItemDto</returns>
    [HttpGet("search")]
    public async Task<PagedResultDto<TodoItemDto>> GetFiltredListAsync(GetTodoItemListDto input)
    {
        return await _sampleAppService.GetFiltredListAsync(input);
    }

    /// <summary>
    /// Get the list of all the TodoItems (applying caching)
    /// </summary>
    /// <returns>TodoItemDto</returns>
    [HttpGet]
    public async Task<List<TodoItemDto>?> GetListAsync()
    {
        return await _sampleAppService.GetListAsync();
    }

    /// <summary>
    /// Get the list of all the TodoItems (applying caching)
    /// </summary>
    /// <returns>TodoItemDto</returns>
    [HttpPost]
    public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto input)
    {
        return await _sampleAppService.CreateAsync(input);
    }

    /// <summary>
    /// Delete a TodoItem by id
    /// </summary>
    /// <returns>TodoItemDto</returns>
    [HttpDelete("{id:guid}")]
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _sampleAppService.DeleteAsync(id);
    }

    /// <summary>
    /// Update a TodoItem by id
    /// </summary>
    /// <returns>TodoItemDto</returns>
    [HttpPut("{id:guid}")]
    public async Task<TodoItemDto> UpdateAsync(Guid id, UpdateTodoItemDto input)
    {
        return await _sampleAppService.UpdateAsync(id, input);
    }

}
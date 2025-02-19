using Abp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListModule.Repositories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace TodoListModule.TodoItems
{
    [RemoteService(false)]
    [UnitOfWork]
    public class TodoAppService : TodoListModuleAppService, ITodoAppService
    {
        #region fields
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly IDistributedCache<List<TodoItemDto>> _distributedCache;
        private readonly ILogger<TodoAppService> _logger;
        private const string CacheKey = "TodoItems";

        #endregion
        public TodoAppService(ITodoItemRepository todoItemRepository,
                              IDistributedCache<List<TodoItemDto>> distributedCache,
                              ILogger<TodoAppService> logger)
        {
            _todoItemRepository = todoItemRepository;
            _distributedCache = distributedCache;
            _logger = logger;
        }
        #region ctor

        #endregion

        #region TodoAppService
        public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto input)
        {
            //Input Validation 
            CheckValidationException(new CreateTodoItemDtoValidator().Validate(input));

            TodoItem item = ObjectMapper.Map<CreateTodoItemDto, TodoItem>(input);
            TodoItem insertedItem = await _todoItemRepository.InsertAsync(item);
            _logger.LogInformation("TodoItem Created with Id: {0}", insertedItem.Id);
            return ObjectMapper.Map<TodoItem, TodoItemDto>(insertedItem);

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            TodoItem item = await _todoItemRepository.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new TodoItemNotFoundException(id);

            await _todoItemRepository.DeleteAsync(item);
            _logger.LogInformation("TodoItem Deleted with Id: {0}", id);
            return true;
        }

        public async Task<TodoItemDto> GetAsync(Guid id)
        {
            TodoItem item = await _todoItemRepository.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new TodoItemNotFoundException(id);

            return ObjectMapper.Map<TodoItem, TodoItemDto>(item);

        }


        /// <summary>
        /// Get All the TodoItems (Implementation using caching)
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoItemDto>?> GetListAsync()
        {
            return await _distributedCache.GetOrAddAsync($"{CacheKey}",
                                   GetAllTodoItemsFromDataBaseAsync,
                                   () => new DistributedCacheEntryOptions
                                   {
                                       AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                                   });

        }

        /// <summary>
        /// Get All the TodoItems using filters
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<TodoItemDto>> GetFiltredListAsync(GetTodoItemListDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Sorting))
            {
                input.Sorting = nameof(TodoItem.Title);
            }
            var listTodoItems = await _todoItemRepository.GetFiltredItemsAsync(input.Filter,
                                                                         input.IsCompleted,
                                                                         input.Sorting,
                                                                         input.SkipCount,
                                                                         input.MaxResultCount);
            var totalCount = await _todoItemRepository
                             .CountAsync(x => (string.IsNullOrWhiteSpace(input.Filter) || x.Title.Contains(input.Filter)) &&
                                              (input.IsCompleted == null || x.IsCompleted == input.IsCompleted));

            return new PagedResultDto<TodoItemDto>(totalCount,
                                                   ObjectMapper.Map<List<TodoItem>,
                                                   List<TodoItemDto>>(listTodoItems));
        }

        public async Task<TodoItemDto> UpdateAsync(Guid id, UpdateTodoItemDto input)
        {
            //Input Validation 
            CheckValidationException(new UpdateTodoItemDtoValidator().Validate(input));

            TodoItem item = await _todoItemRepository.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new TodoItemNotFoundException(id);

            ObjectMapper.Map(input, item);
            await _todoItemRepository.UpdateAsync(item, autoSave: true);
            _logger.LogInformation("TodoItem Updated with Id: {0}", id);
            return ObjectMapper.Map<TodoItem, TodoItemDto>(item);
        }

        #endregion

        #region Private Methods
        private async Task<List<TodoItemDto>> GetAllTodoItemsFromDataBaseAsync()
        {
            return ObjectMapper.Map<List<TodoItem>, List<TodoItemDto>>(await _todoItemRepository.GetListAsync());

        }
        #endregion

    }
}

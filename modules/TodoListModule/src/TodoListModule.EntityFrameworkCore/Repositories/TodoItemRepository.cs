using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TodoListModule.TodoItems;
using TodoListModule.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace TodoListModule.Repositories
{
    public class TodoItemRepository : EfCoreRepository<TodoListModuleDbContext, TodoItem, Guid>, ITodoItemRepository
    {
        public TodoItemRepository(IDbContextProvider<TodoListModuleDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<List<TodoItem>> GetFiltredItemsAsync(string filter, bool? isCompleted, string sorting, int skipCount, int take)
        {
            return await (await GetDbSetAsync())
                .WhereIf(isCompleted is not null, t => t.IsCompleted == isCompleted)
                .WhereIf(!string.IsNullOrWhiteSpace(filter), t => t.Title.Contains(filter))
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(take)
                .ToListAsync();
        }

    }
}

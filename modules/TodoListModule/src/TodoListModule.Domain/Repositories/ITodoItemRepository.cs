using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListModule.TodoItems;
using Volo.Abp.Domain.Repositories;

namespace TodoListModule.Repositories
{

    public interface ITodoItemRepository : IRepository<TodoItem, Guid>
    {
        Task<List<TodoItem>> GetFiltredItemsAsync(string filter, bool? isCompleted, string sorting, int skipCount, int take);
    }

}

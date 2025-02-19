using AutoMapper;
using TodoListModule.TodoItems;

namespace TodoListModule;

public class TodoListModuleApplicationAutoMapperProfile : Profile
{
    public TodoListModuleApplicationAutoMapperProfile()
    {
        CreateMap<TodoItem, TodoItemDto>();
        CreateMap<CreateTodoItemDto, TodoItem>(MemberList.Source);
        CreateMap<UpdateTodoItemDto, TodoItem>(MemberList.Source);
    }
}

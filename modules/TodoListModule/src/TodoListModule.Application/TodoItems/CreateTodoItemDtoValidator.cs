using FluentValidation;

namespace TodoListModule.TodoItems
{
    public class CreateTodoItemDtoValidator : AbstractValidator<CreateTodoItemDto>
    {
        public CreateTodoItemDtoValidator()
        {
           RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(TodoListModuleConsts.TitleMaxLength).WithMessage("Invalid Title");
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(TodoListModuleConsts.DescriptionMaxLength).WithMessage("Invalid Title");
        }
    }
}

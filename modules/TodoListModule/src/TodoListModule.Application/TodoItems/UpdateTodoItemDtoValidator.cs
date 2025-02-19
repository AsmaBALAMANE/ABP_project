using FluentValidation;

namespace TodoListModule.TodoItems
{
   public class UpdateTodoItemDtoValidator : AbstractValidator<UpdateTodoItemDto>
    {
        public UpdateTodoItemDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(TodoListModuleConsts.TitleMaxLength).WithMessage("Invalid Title");
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(TodoListModuleConsts.DescriptionMaxLength).WithMessage("Invalid Description");
            RuleFor(x => x.IsCompleted).NotNull().WithMessage("IsComplete should not be null"); ;

        }
    }
}

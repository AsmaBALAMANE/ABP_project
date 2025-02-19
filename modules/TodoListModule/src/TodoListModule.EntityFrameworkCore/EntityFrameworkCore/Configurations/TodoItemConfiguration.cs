using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListModule.TodoItems;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace TodoListModule.EntityFrameworkCore.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.ConfigureByConvention();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(TodoListModuleConsts.TitleMaxLength);
            builder.Property(x => x.Description).HasMaxLength(TodoListModuleConsts.DescriptionMaxLength);
            builder.Property(x => x.IsCompleted).HasDefaultValue(false);
            builder.ToTable("TodoItems", TodoListModuleConsts.TodoListSchema);
        }
    }
}

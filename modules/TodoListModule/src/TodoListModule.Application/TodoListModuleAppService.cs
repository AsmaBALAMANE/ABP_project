using Abp.AspNetCore.Mvc.ExceptionHandling;
using System.Linq;
using TodoListModule.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.Validation;

namespace TodoListModule;

public abstract class TodoListModuleAppService : ApplicationService
{
    protected TodoListModuleAppService()
    {
        LocalizationResource = typeof(TodoListModuleResource);
        ObjectMapperContext = typeof(TodoListModuleApplicationModule);
    }

    #region Helper methods
    protected void CheckValidationException(FluentValidation.Results.ValidationResult validationResult)
    {
        if (!validationResult.IsValid)
        {
            var message = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            var errors =
                validationResult
                .Errors
                .Select(x => new System.ComponentModel.DataAnnotations.ValidationResult
                                (x.ErrorMessage, [x.PropertyName]))
                .ToList();

            throw new AbpValidationException(message, errors);
        }


    }
    #endregion
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
     public static void Validate<T>(IValidator validator,T entity) where T:class,new()
        {
            var result = validator.Validate(new ValidationContext<T>(entity));
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}

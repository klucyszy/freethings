using System.ComponentModel.DataAnnotations;

namespace Freethings.Shared.Infrastructure.Api;

public static class ModelValidationExtensions
{
    public static bool TryValidate(this IValidatableObject model, out IEnumerable<ValidationResult> results)
    {
        ValidationContext context = new(model, serviceProvider: null, items: null);
        results = model.Validate(context);
        
        return Validator.TryValidateObject(model, context, results.ToHashSet(), validateAllProperties: true);
    }
}
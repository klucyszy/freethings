using System.ComponentModel.DataAnnotations;
using Freethings.Shared.Infrastructure.Api.Results;

namespace Freethings.Shared.Infrastructure.Api.Filters;

public static class InputValidationFilterExtensions
{
    public static RouteHandlerBuilder RequireInputValidation(this RouteHandlerBuilder builder)
    {
        builder.AddEndpointFilter(new InputValidationFilter());

        return builder;
    }
}

internal sealed class InputValidationFilter : IEndpointFilter
{
    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        List<IValidatableObject> validatableArgs = context.Arguments
            .OfType<IValidatableObject>()
            .ToList();

        List<ValidationResult> validationResults = new();
        foreach (IValidatableObject validatable in validatableArgs)
        {
            validatable.TryValidate(out IEnumerable<ValidationResult> results);
            validationResults.AddRange(results);
        }

        if (validationResults.Any())
        {
            return EndpointResult.ValidationErrorResult(validationResults);
        }
        
        return await next(context);
    }
}
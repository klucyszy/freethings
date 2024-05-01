using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Shared.Infrastructure.Api.Results;

public static class EndpointResult
{
    private const string ValidationErrorKey = "validationErrors";
    private const string BusinessErrorKey = "businessErrors";
    
    public static IResult ValidationErrorResult(IEnumerable<ValidationResult> validationResults)
    {
        ValidationProblemDetails problemDetails = new()
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Input validation failed",
            Errors = new Dictionary<string, string[]>()
            {
                { ValidationErrorKey, validationResults.Select(vr => vr.ErrorMessage).ToArray()}
            }
        };

        return TypedResults.Problem(problemDetails);
    }
}
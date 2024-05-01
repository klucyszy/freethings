using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Shared.Infrastructure.Api.Results;

public static class ApiResultMapper
{
    public static IResult MapToEndpointResult(BusinessResult result)
    {
        return result.IsSuccess
            ? Microsoft.AspNetCore.Http.Results.NoContent()
            : Microsoft.AspNetCore.Http.Results.Problem(
                result.ErrorMessage,
                statusCode: StatusCodes.Status400BadRequest);
    }
    
    public static IResult MapToEndpointResult<T>(BusinessResult<T> result)
    {
        return result.IsSuccess
            ? Microsoft.AspNetCore.Http.Results.Created(string.Empty, result.Data)
            : Microsoft.AspNetCore.Http.Results.Problem(
                result.ErrorMessage,
                statusCode: StatusCodes.Status400BadRequest);
    }
}
using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Shared.Infrastructure.Api;

public static class ApiResultMapper
{
    public static IResult MapToEndpointResult(BusinessResult result)
    {
        return result.IsSuccess
            ? Results.NoContent()
            : Results.Problem(
                result.ErrorMessage,
                statusCode: StatusCodes.Status400BadRequest);
    }
    
    public static IResult MapToEndpointResult<T>(BusinessResult<T> result)
    {
        return result.IsSuccess
            ? Results.Created(string.Empty, result.Data)
            : Results.Problem(
                result.ErrorMessage,
                statusCode: StatusCodes.Status400BadRequest);
    }
}
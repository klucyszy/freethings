using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Microsoft.AspNetCore.Diagnostics;

namespace Freethings.Shared.Infrastructure.Middleware;

internal sealed class BusinessExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not BusinessException businessException)
        {
            return false;
        }
        
        IResult problemResult = Results.Problem(
            businessException.Message,
            statusCode: StatusCodes.Status400BadRequest
        );
        
        await problemResult.ExecuteAsync(httpContext);

        return true;
    }
}
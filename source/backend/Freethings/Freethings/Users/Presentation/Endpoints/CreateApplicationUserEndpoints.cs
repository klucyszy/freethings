using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Infrastructure.Api;
using Freethings.Users.Application.Commands;

namespace Freethings.Users.Presentation.Endpoints;

public static class CreateApplicationUser
{
    private sealed record Body(string Auth0UserIdentifier);

    public static RouteGroupBuilder MapCreateApplicationUserEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("", async (
                    Body requestBody,
                    ISender sender,
                    CancellationToken ct)
                =>
            {
                BusinessResult result = await sender.Send(new CreateApplicationUserCommand(
                    requestBody.Auth0UserIdentifier), ct);

                return ApiResultMapper.MapToEndpointResult(result);
            });

        return group;
    }
}
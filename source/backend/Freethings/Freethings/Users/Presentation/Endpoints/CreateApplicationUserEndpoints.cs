using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Infrastructure.Api.Results;
using Freethings.Shared.Infrastructure.Authentication;
using Freethings.Users.Application.Commands;

namespace Freethings.Users.Presentation.Endpoints;

public static class CreateApplicationUser
{
    private sealed record Body(string Auth0UserIdentifier, string Username);

    public static RouteGroupBuilder MapCreateApplicationUserEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("", async (
                    Body body,
                    ISender sender,
                    CancellationToken ct)
                =>
            {
                BusinessResult result = await sender.Send(new CreateApplicationUserCommand(
                    body.Auth0UserIdentifier, body.Username), ct);

                return ApiResultMapper.MapToEndpointResult(result);
            })
            .RequireAuthorization(builder =>
            {
                builder.AuthenticationSchemes.Add(AuthSchemas.ApiKey);
            });

        return group;
    }
}
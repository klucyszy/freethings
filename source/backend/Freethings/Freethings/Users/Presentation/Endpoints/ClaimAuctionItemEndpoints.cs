using Freethings.Auctions.Application.Commands;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Infrastructure.Api;
using Freethings.Users.Application.Commands;

namespace Freethings.Users.Presentation.Endpoints;

public static class CreateApplicationUser
{
    private sealed record QueryParameters(
        string Auth0UserIdentifier);

    public static RouteGroupBuilder MapClaimAuctionItemEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("", async (
                    [AsParameters] QueryParameters parameters,
                    ISender sender,
                    CancellationToken ct)
                =>
            {
                BusinessResult result = await sender.Send(new CreateApplicationUserCommand(
                    parameters.Auth0UserIdentifier), ct);

                return ApiResultMapper.MapToEndpointResult(result);
            });

        return group;
    }
}
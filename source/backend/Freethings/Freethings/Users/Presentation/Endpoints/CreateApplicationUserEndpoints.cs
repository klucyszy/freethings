using System.ComponentModel.DataAnnotations;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Infrastructure.Api.Results;
using Freethings.Shared.Infrastructure.Authentication.ApiKey;
using Freethings.Users.Application.Commands;

namespace Freethings.Users.Presentation.Endpoints;

public static class CreateApplicationUser
{
    private sealed record CreateApplicationUserRequestBody(
        string Auth0UserIdentifier,
        string Username) : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Auth0UserIdentifier))
            {
                yield return new ValidationResult($"{nameof(Auth0UserIdentifier)} is required");
            }

            if (string.IsNullOrWhiteSpace(Username))
            {
                yield return new ValidationResult($"{nameof(Username)} is required");
            }
        }
    };

    public static RouteGroupBuilder MapCreateApplicationUserEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("", async (
                    CreateApplicationUserRequestBody body,
                    ISender sender,
                    CancellationToken ct)
                =>
            {
                BusinessResult result = await sender.Send(new CreateApplicationUserCommand(
                    body.Auth0UserIdentifier, body.Username), ct);

                return ApiResultMapper.MapToEndpointResult(result);
            })
            .RequireApiKeyAuthorization();

        return group;
    }
}
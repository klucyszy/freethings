using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Users.Application.Services;

namespace Freethings.Users.Application.Commands;

public sealed record CreateApplicationUserCommand : IRequest<BusinessResult>
{
    public string Auth0UserIdentifier { get; init; }

    public CreateApplicationUserCommand(string auth0UserIdentifier)
    {
        Auth0UserIdentifier = auth0UserIdentifier;
    }
}

internal sealed class CreateApplicationUserCommanddHandler : IRequestHandler<CreateApplicationUserCommand, BusinessResult>
{
    private readonly IIdentityProviderSerivce _identityProviderSerivce;

    public CreateApplicationUserCommanddHandler(IIdentityProviderSerivce identityProviderSerivce)
    {
        _identityProviderSerivce = identityProviderSerivce;
    }

    public async Task<BusinessResult> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        await _identityProviderSerivce.SaveUserIdAsync(
            request.Auth0UserIdentifier,
            Guid.NewGuid().ToString(), cancellationToken);

        return BusinessResult.Success();
    }
}
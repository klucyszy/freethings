using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Users.Application.Services;
using Freethings.Users.Domain;
using Freethings.Users.Domain.Repositories;

namespace Freethings.Users.Application.Commands;

public sealed record CreateApplicationUserCommand : IRequest<BusinessResult>
{
    public string Auth0UserIdentifier { get; init; }
    public string Username { get; init; }

    public CreateApplicationUserCommand(string auth0UserIdentifier, string username)
    {
        Auth0UserIdentifier = auth0UserIdentifier;
        Username = username;
    }
}

internal sealed class CreateApplicationUserCommanddHandler : IRequestHandler<CreateApplicationUserCommand, BusinessResult>
{
    private readonly IIdentityProviderService _identityProviderService;
    private readonly IUserRepository _userRepository;

    public CreateApplicationUserCommanddHandler(
        IIdentityProviderService identityProviderService,
        IUserRepository userRepository)
    {
        _identityProviderService = identityProviderService;
        _userRepository = userRepository;
    }

    public async Task<BusinessResult> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        bool userAlreadyExists = await _userRepository
            .ExistsAsync(request.Auth0UserIdentifier, cancellationToken);

        if (userAlreadyExists)
        {
            return BusinessResult.Failure("User already created");
        }

        User user = new User(
            request.Auth0UserIdentifier,
            request.Username
        );

        user = await _userRepository.AddAsync(user, cancellationToken);
        
        await _identityProviderService.SaveUserIdAsync(
            request.Auth0UserIdentifier,
            user.Id.ToString(), cancellationToken);

        return BusinessResult.Success();
    }
}
using Freethings.PublicApi.Events.Users;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Abstractions.Messaging;
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
    private readonly IEventBus _eventBus;
    private readonly ICurrentTime _currentTime;

    public CreateApplicationUserCommanddHandler(
        IIdentityProviderService identityProviderService,
        IUserRepository userRepository,
        IEventBus eventBus, ICurrentTime currentTime)
    {
        _identityProviderService = identityProviderService;
        _userRepository = userRepository;
        _eventBus = eventBus;
        _currentTime = currentTime;
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
            request.Username,
            _currentTime.UtcNow()
        );

        user = await _userRepository.AddAsync(user, cancellationToken);
        
        await _identityProviderService.SaveUserIdAsync(
            request.Auth0UserIdentifier,
            user.Id.ToString(), cancellationToken);
        
        await _eventBus.PublishAsync(new UserEvent.UserCreated(
                user.Id,
                user.CreatedAt), cancellationToken);

        return BusinessResult.Success();
    }
}
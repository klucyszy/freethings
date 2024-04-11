using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Freethings.Users.Application.Services;
using Microsoft.Extensions.Options;

namespace Freethings.Users.Infrastructure.Auth0;

internal sealed class Auth0ManagementClientService : IIdentityProviderService
{
    private readonly ManagementApiClient _managementApiClient;

    public Auth0ManagementClientService(IOptions<Auth0Options> options)
    {
        _managementApiClient = new ManagementApiClient(
            options.Value.ManagementApiToken,
            new Uri(options.Value.BaseUrl));
    }

    public async Task SaveUserIdAsync(string identiy, string appUserId, CancellationToken cancellationToken = default)
    {
        await _managementApiClient.Users.UpdateAsync(identiy, new UserUpdateRequest
        {
            AppMetadata = new
            {
                appUserId = appUserId
            }
        }, cancellationToken);
    }
}
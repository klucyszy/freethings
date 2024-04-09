using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Freethings.Users.Application.Services;

namespace Freethings.Users.Infrastructure.Auth0;

internal sealed class Auth0ManagementClientService : IIdentityProviderSerivce
{
    private readonly ManagementApiClient _managementApiClient;

    public Auth0ManagementClientService(ManagementApiClient managementApiClient)
    {
        _managementApiClient = new ManagementApiClient(
            "token",
            new Uri("https://dev-vf8eeqf11x3ud8nu.us.auth0.com/api/v2/")
        );
    }

    public async Task SaveUserIdAsync(string identiy, string appUserId, CancellationToken cancellationToken = default)
    {
        await _managementApiClient.Users.UpdateAsync(identiy, new UserUpdateRequest
        {
            AppMetadata = new
            {
                appUserId
            }
        }, cancellationToken);
    }
}
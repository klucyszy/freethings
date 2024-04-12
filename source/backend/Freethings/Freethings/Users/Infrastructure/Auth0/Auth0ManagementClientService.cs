using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Freethings.Users.Application.Services;
using Microsoft.Extensions.Options;

namespace Freethings.Users.Infrastructure.Auth0;

internal sealed class Auth0ManagementClientService : IIdentityProviderService
{
    private readonly Auth0Options _options;
    
    public Auth0ManagementClientService(IOptions<Auth0Options> options)
    {
        _options = options.Value;
    }

    public async Task SaveUserIdAsync(string identiy, string appUserId, CancellationToken cancellationToken = default)
    {
        using ManagementApiClient client = new(await GetTokenAsync(), new Uri(_options.BaseUrl));
        await client.Users.UpdateAsync(identiy, new UserUpdateRequest
        {
            AppMetadata = new
            {
                appUserId = appUserId
            }
        }, cancellationToken);
    }
    
    private async Task<string> GetTokenAsync()
    {
        AuthenticationApiClient authenticationApiClient = new(new Uri(_options.BaseUrl));

        ClientCredentialsTokenRequest tokenRequest = new ClientCredentialsTokenRequest
        {
            ClientId = _options.ClientId,
            ClientSecret = _options.ClientSecret,
            Audience = _options.Audience
        };

        AccessTokenResponse tokenResponse = await authenticationApiClient.GetTokenAsync(tokenRequest);

        return tokenResponse.AccessToken;
    }
}
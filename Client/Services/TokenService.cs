using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Client.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<ServerSettings> _identityServerSettings;
        private readonly DiscoveryDocumentResponse _discoveryDocument;
        private readonly HttpClient _client;

        public TokenService(IOptions<ServerSettings> identityServerSettings) 
        {
            _identityServerSettings = identityServerSettings;
            _client = new();
            _discoveryDocument = _client.GetDiscoveryDocumentAsync(_identityServerSettings.Value.DiscoveryUrl).Result;

            if (_discoveryDocument.IsError)
            {
                throw new Exception("unable to get discovery document",_discoveryDocument.Exception);
            }
        }
        public async Task<TokenResponse> GetToken(string scope)
        {
            var tokenResponse = await _client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoveryDocument.TokenEndpoint,
                ClientId =_identityServerSettings.Value.ClientName,
                ClientSecret = _identityServerSettings.Value.ClientPassword,
                Scope = scope
            });

            if (tokenResponse.IsError)
            {
                throw new Exception("Unable to get token", tokenResponse.Exception);
            }

            return tokenResponse;
        }
    }
}

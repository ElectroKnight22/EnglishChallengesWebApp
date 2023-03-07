using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;

namespace EnglishChallengesWebApp.Resources.Auth0
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(
            IAccessTokenProvider provider,
            NavigationManager navigation)
            : base(provider, navigation)
        {
            var url = "https://dev-nnaqm6y48c285fga.eu.auth0.com/api/v2/"; // <-- insert your value here
            ConfigureHandler(authorizedUrls: new[] { url });
        }
    }
}

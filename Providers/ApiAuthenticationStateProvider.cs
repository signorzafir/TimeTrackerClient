using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TimeTrackerClient.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //default principle in case of empty identity for not logged in
            var user = new ClaimsPrincipal(new ClaimsIdentity());

            //retreive stored token from local Storage
            var savedToken = await localStorage.GetItemAsync<string>("accessToken");

            //In case of no stored token, return a new authState as not logged in principle
            if (savedToken == null)
            {
                return new AuthenticationState(user);
            }

            //if there is saved token, read into tokenContent as jwtSecurityToken
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);

            //if stored Token is expired that return user as not logged in principal
            if (tokenContent.ValidTo < DateTime.Now) 
            {
                return new AuthenticationState(user);
            }

            // if we have valid savedToken then we set claims and update user
            var claims = await GetClaims();
            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            return new AuthenticationState(user);
        }

        //Method to call when logged in
        public async Task LoggedIn()
        {
            var claims = await GetClaims();
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        

        //Method to call if logged out
        public async Task LoggedOut()
        {
            await localStorage.RemoveItemAsync("accessToken");
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }
        public async Task<List<Claim>?> GetClaims()
        {
            var savedToken = await localStorage.GetItemAsync<string>("accessToken");
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}

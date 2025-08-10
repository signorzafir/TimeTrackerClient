using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using TimeTrackerClient.Providers;
using TimeTrackerClient.Services.Base;

namespace TimeTrackerClient.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient,
                                     ILocalStorageService localStorage,
                                     AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
        {
            var response = await httpClient.LoginAsync(loginModel);

            //storing Tokken
            await localStorage.SetItemAsync("accessToken", response.Token);

            //Change Auth State of the application, Using Custom ApiAuthStateProvider
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();
            return true;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}

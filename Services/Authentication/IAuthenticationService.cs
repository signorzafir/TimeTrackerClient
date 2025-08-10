using TimeTrackerClient.Services.Base;

namespace TimeTrackerClient.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);
        public Task Logout();
    }
}

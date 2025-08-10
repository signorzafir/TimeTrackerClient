using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TimeTrackerClient.Providers;
using TimeTrackerClient.Services.Authentication;
using TimeTrackerClient.Services.Base;
using TimeTrackerClient.Services.Employee;
using TimeTrackerClient.Services.WorkEntry;


namespace TimeTrackerClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7043/") });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
                sp.GetRequiredService<ApiAuthenticationStateProvider>());
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<IClient, Client>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IWorkEntryService, WorkEntryService>();


            await builder.Build().RunAsync();
        }
    }
}

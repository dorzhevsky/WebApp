using Microsoft.Extensions.DependencyInjection;
using Users.HostedServices.Impl;
using Users.HostedServices.Interfaces;

namespace Users.HostedServices
{
    public static class Setup
    {
        public static void RegisterHostedServices(this IServiceCollection services)
        {
            services.AddSingleton<IUsersHostedService, UsersHostedService>();
            services.AddHostedService(sp => (UsersHostedService)sp.GetRequiredService<IUsersHostedService>());
        }
    }
}

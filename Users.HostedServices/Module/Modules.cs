using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularize;
using Users.HostedServices.Impl;
using Users.HostedServices.Interfaces;

namespace Users.HostedServices.Module
{
    public class Modules
    {
        public class ServicesModule : IServicesModule
        {
            public void RegisterServices(IServiceCollection services, IConfiguration configuration)
            {
                services.AddSingleton<IUsersHostedService, UsersHostedService>();
                services.AddHostedService(sp => (UsersHostedService)sp.GetRequiredService<IUsersHostedService>());
            }
        }
    }
}

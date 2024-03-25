using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularize;
using Users.Services.Interfaces;
using Users.Services.Services;

namespace Users.Services.Module
{
    public class Modules
    {
        public class ServicesModule : IServicesModule
        {
            public void RegisterServices(IServiceCollection services, IConfiguration configuration)
            {
                services.Scan(scan => scan
                                .FromAssemblies(typeof(IUsersService).Assembly)
                                .AddClasses(f => f.AssignableTo<IService>())
                                .AsImplementedInterfaces()
                                .WithTransientLifetime());

                services.AddSingleton<IUsersHostedService, UsersHostedService>();
                services.AddHostedService(sp => (UsersHostedService)sp.GetRequiredService<IUsersHostedService>());
            }
        }
    }
}

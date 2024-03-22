using Microsoft.Extensions.DependencyInjection;
using Users.Application.Interfaces;

namespace Users.Application
{
    public static class Setup
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
                            .FromAssemblies(typeof(IUsersService).Assembly)
                            .AddClasses(f => f.AssignableTo<IService>())
                            .AsImplementedInterfaces()
                            .WithTransientLifetime());
        }
    }
}

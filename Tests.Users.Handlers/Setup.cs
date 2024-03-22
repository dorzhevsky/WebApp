using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application;
using Users.Postgres;

namespace Users.Handlers.Tests
{
    internal class Setup
    {
        public static ServiceProvider Init(Action<IServiceCollection> afterAction)
        {
            Users.Handlers.Setup.RegisterMappings();

            IServiceCollection services = new ServiceCollection();

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services.AddSingleton(config);
            services.RegisterServices();
            services.RegisterPostgres(config);

            afterAction(services);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}

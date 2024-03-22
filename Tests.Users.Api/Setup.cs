using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application;
using Users.Postgres;

namespace Tests.Users.Api
{
    internal class Setup
    {
        public static ServiceProvider Init(Action<IServiceCollection> afterAction)
        {
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

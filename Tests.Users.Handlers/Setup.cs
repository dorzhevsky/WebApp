using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularize;

namespace Users.Handlers.Tests
{
    internal class Setup
    {
        public static ServiceProvider Init(Action<IServiceCollection> afterAction)
        {
            IServiceCollection services = new ServiceCollection();

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services.AddSingleton(config);

            services.AddModularizer(config,
                typeof(Services.Modularize.Modules).Assembly,
                typeof(Postgres.Modularize.Modules).Assembly,
                typeof(Handlers.Modularize.Modules).Assembly
            );

            afterAction(services);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}

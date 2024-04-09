using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Modularize;

namespace Modules.Users.Adapter.Handlers.Tests
{
    internal class Setup
    {
        public static ServiceProvider Init(Action<IServiceCollection> afterAction)
        {
            IServiceCollection services = new ServiceCollection();

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services.AddSingleton(config);

            services.AddModularizer(config,
                typeof(Core.Services.Modularize.Modules).Assembly,
                typeof(Modularize.Modules).Assembly,
                typeof(Postgres.Modularize.Modules).Assembly
            );

            afterAction(services);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}

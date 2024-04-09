using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pg = Modules.Users.Adapter.Postgres.Modularize;
using Svc = Modules.Users.Core.Services.Modularize;
using Shared.Modularize;
using Shared.LoggerManager;

namespace Modules.Users.Adapter.Api.Tests
{
    internal class Setup
    {
        public static ServiceProvider Init(Action<IServiceCollection> afterAction)
        {
            IServiceCollection services = new ServiceCollection();

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services.AddSingleton(config);
            services.AddSingleton<ILoggingManager, LoggingManager>();

            services.AddModularizer(config,
                typeof(Pg.Modules).Assembly,
                typeof(Svc.Modules).Assembly,
                typeof(Handlers.Modularize.Modules).Assembly
            );

            afterAction(services);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}

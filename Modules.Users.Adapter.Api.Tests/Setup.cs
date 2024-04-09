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
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(config)
                    .AddSingleton<ILoggingManager, LoggingManager>()
                    .AddModularizer(config,
                        typeof(Pg.Modules).Assembly,
                        typeof(Svc.Modules).Assembly,
                        typeof(Handlers.Modularize.Modules).Assembly
            );

            afterAction(services);

            return services.BuildServiceProvider();
        }
    }
}

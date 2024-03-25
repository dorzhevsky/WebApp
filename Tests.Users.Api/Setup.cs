using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postgres = Users.Postgres.Module;
using Svc = Users.Services.Module;
using Handlers = Users.Handlers.Module;
using Modularize;

namespace Tests.Users.Api
{
    internal class Setup
    {
        public static ServiceProvider Init(Action<IServiceCollection> afterAction)
        {
            IServiceCollection services = new ServiceCollection();

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services.AddSingleton(config);

            services.AddModularizer(config,
                typeof(Postgres.Modules).Assembly,
                typeof(Svc.Modules).Assembly,
                typeof(Handlers.Modules).Assembly
            );

            afterAction(services);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}

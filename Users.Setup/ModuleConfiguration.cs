using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module2.Api;
using Modularize;
using Users.Handlers;
using Users.Messaging;
using Users.HostedServices;
using Users.Application;
using Users.Postgres;

namespace Users.Setup
{
    public class ModuleConfiguration : IConfigurationModule
    {
        public void RegisterControllers(IMvcBuilder builder, IConfiguration _configuration)
        {
            builder.AddApp();
        }

        public void RegisterMediatrHandlers(MediatRServiceConfiguration cfg, IConfiguration configuration)
        {
            cfg.RegisterMediatrHandlers();
        }

        public void RegisterRebusHandlers(IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterRebusHandlers();
        }

        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterHostedServices();
            services.RegisterServices();
            services.RegisterPostgres(configuration);
        }

        public void RegisterMappings(IConfiguration configuration)
        {
            Handlers.Setup.RegisterMappings();
        }
    }
}

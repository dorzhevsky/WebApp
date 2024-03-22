using External.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularize;

namespace External.Setup
{
    public class ModuleConfiguration : IConfigurationModule
    {
        public void RegisterControllers(IMvcBuilder builder, IConfiguration _configuration)
        {
        }

        public void RegisterMappings(IConfiguration configuration)
        {
        }

        public void RegisterMediatrHandlers(MediatRServiceConfiguration cfg, IConfiguration configuration)
        {
            cfg.RegisterServicesFromAssembly(typeof(GetExternalDataHandler).Assembly);
        }

        public void RegisterRebusHandlers(IServiceCollection services, IConfiguration configuration)
        {
        }
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}

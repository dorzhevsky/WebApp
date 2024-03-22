using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modularize
{
    public interface IConfigurationModule
    {
        void RegisterControllers(IMvcBuilder builder, IConfiguration _configuration);
        void RegisterMediatrHandlers(MediatRServiceConfiguration cfg, IConfiguration configuration);
        void RegisterRebusHandlers(IServiceCollection services, IConfiguration configuration);
        void RegisterServices(IServiceCollection services, IConfiguration configuration);
        void RegisterMappings(IConfiguration configuration);
    }
}

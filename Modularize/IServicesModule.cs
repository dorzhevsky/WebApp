using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modularize
{
    public interface IServicesModule
    {
        void RegisterServices(IServiceCollection services, IConfiguration configuration);
    }
}

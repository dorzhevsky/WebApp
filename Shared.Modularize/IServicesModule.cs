using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Modularize
{
    public interface IServicesModule
    {
        void RegisterServices(IServiceCollection services, IConfiguration configuration);
    }
}

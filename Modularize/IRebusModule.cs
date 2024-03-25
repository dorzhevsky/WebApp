using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modularize
{
    public interface IRebusModule
    {
        void RegisterRebusHandlers(IServiceCollection services, IConfiguration configuration);
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Modularize
{
    public interface IRebusModule
    {
        void RegisterRebusHandlers(IServiceCollection services, IConfiguration configuration);
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Modularize
{
    public interface IControllersModule
    {
        void RegisterControllers(IMvcBuilder builder, IConfiguration configuration);
    }
}

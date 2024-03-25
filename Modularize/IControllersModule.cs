using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modularize
{
    public interface IControllersModule
    {
        void RegisterControllers(IMvcBuilder builder, IConfiguration _configuration);
    }
}

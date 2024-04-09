using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.External.Adapter.Api.External;
using Shared.Modularize;

namespace Modules.External.Adapter.Api.Modularize
{
    public class Modules
    {
        public class ControllersModule : IControllersModule
        {
            public void RegisterControllers(IMvcBuilder builder, IConfiguration _configuration)
            {
                builder.AddApplicationPart(typeof(ExternalController).Assembly);
            }
        }
    }
}

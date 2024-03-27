using External.Adapter.Api.External;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularize;

namespace External.Adapter.Api.Modularize
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

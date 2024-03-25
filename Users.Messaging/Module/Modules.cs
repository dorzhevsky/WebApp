using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularize;
using Rebus.Config;
using Users.Messaging.Impl;

namespace Users.Messaging.Module
{
    public class Modules
    {
        public class ServicesModule : IServicesModule
        {
            public void RegisterServices(IServiceCollection services, IConfiguration configuration)
            {
                services.AutoRegisterHandlersFromAssembly(typeof(ProcessUsersHandler).Assembly);
            }
        }
    }
}

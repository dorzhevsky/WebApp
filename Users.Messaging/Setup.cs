using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Users.Messaging.Impl;

namespace Users.Messaging
{
    public static class Setup
    {
        public static void RegisterRebusHandlers(this IServiceCollection services)
        {
            services.AutoRegisterHandlersFromAssembly(typeof(ProcessUsersHandler).Assembly);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Adapter.Api.Users;
using Shared.Modularize;

namespace Modules.Users.Adapter.Api.Modularize
{
    public class Modules
    {
        public class ControllersModule : IControllersModule
        {
            public void RegisterControllers(IMvcBuilder builder, IConfiguration _configuration)
            {
                builder.AddApplicationPart(typeof(UsersController).Assembly);
            }
        }
    }
}

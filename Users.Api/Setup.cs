using Microsoft.Extensions.DependencyInjection;
using Users.Api.Users;

namespace Module2.Api
{
    public static class Setup
    {
        public static void AddApp(this IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(UsersController).Assembly);
        }

        public static void RegisterMappings()
        {
        }
    }
}

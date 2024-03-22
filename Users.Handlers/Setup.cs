using Microsoft.Extensions.DependencyInjection;
using Nelibur.ObjectMapper;
using Users.Contracts;
using Users.Domain;

namespace Users.Handlers
{
    public static class Setup
    {
        public static void RegisterMediatrHandlers(this MediatRServiceConfiguration cfg)
        {
            cfg.RegisterServicesFromAssemblies(typeof(UsersCommandHandler).Assembly);
        }

        public static void RegisterMappings()
        {
            TinyMapper.Bind<User, UserDto>();
        }
    }
}

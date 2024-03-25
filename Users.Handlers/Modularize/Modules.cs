using Contracts.Modules.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularize;
using Nelibur.ObjectMapper;
using Rebus.Config;
using Users.Domain;

namespace Users.Handlers.Modularize
{
    public class Modules
    {
        public class MediatrModule : IMediatrModule
        {
            public void RegisterMediatrHandlers(MediatRServiceConfiguration cfg, IConfiguration configuration)
            {
                cfg.RegisterServicesFromAssemblies(typeof(ExternalNotificationdHandler).Assembly);
            }
        }

        public class MappingModule : IMappingModule
        {
            public void RegisterMappings(IConfiguration configuration)
            {
                TinyMapper.Bind<User, UserDto>();
            }
        }

        public class ServicesModule : IServicesModule
        {
            public void RegisterServices(IServiceCollection services, IConfiguration configuration)
            {
                services.AutoRegisterHandlersFromAssembly(typeof(ProcessUsersHandler).Assembly);
            }
        }
    }
}

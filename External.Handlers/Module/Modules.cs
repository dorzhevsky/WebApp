using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularize;

namespace External.Handlers.Module
{
    public class Modules
    {
        public class MediatrModule : IMediatrModule
        {
            public void RegisterMediatrHandlers(MediatRServiceConfiguration cfg, IConfiguration configuration)
            {
                cfg.RegisterServicesFromAssemblies(typeof(GetExternalDataHandler).Assembly);
            }
        }
    }
}

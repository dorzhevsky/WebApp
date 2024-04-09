using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Modularize;

namespace Modules.External.Adapter.Handlers.Modularize
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

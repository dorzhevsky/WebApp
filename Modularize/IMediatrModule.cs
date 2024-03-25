using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modularize
{
    public interface IMediatrModule
    {
        void RegisterMediatrHandlers(MediatRServiceConfiguration cfg, IConfiguration configuration);
    }
}

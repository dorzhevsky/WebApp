using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Modularize
{
    public interface IMediatrModule
    {
        void RegisterMediatrHandlers(MediatRServiceConfiguration cfg, IConfiguration configuration);
    }
}

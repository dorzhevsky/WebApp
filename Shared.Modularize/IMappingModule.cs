using Microsoft.Extensions.Configuration;

namespace Shared.Modularize
{
    public interface IMappingModule
    {
        void RegisterMappings(IConfiguration configuration);
    }
}

using Microsoft.Extensions.Configuration;

namespace Modularize
{
    public interface IMappingModule
    {
        void RegisterMappings(IConfiguration configuration);
    }
}

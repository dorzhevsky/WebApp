using External.Api.External;
using Microsoft.Extensions.DependencyInjection;

namespace External.Api
{
    public static class Setup
    {
        public static void AddApp(this IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(ExternalController).Assembly);
        }

        public static void RegisterMappings()
        {
        }
    }
}

using Microsoft.Extensions.Configuration;

namespace Modularize
{
    public static class Modularizer
    {
        public static IEnumerable<IConfigurationModule> LoadFromConfiguration(IConfiguration configuration)
        {
            ModuleOptions? modules = configuration.Get<ModuleOptions>();

            if (modules is not null)
            {
                return modules.Modules.Select(e =>
                {
                    Type? type = Type.GetType(e.Type);
                    return type is not null ? (IConfigurationModule?)Activator.CreateInstance(type) : null;
                }).Where(e => e is not null)
                .Cast<IConfigurationModule>();
            }

            return Enumerable.Empty<IConfigurationModule>();
        }
    }
}

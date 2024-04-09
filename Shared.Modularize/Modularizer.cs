using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Modularize
{
    public static class Modularizer
    {
        public static void AddModularizer(this IServiceCollection services,
                                          IConfiguration configuration,
                                          params Assembly[] assemblies)
        {
            var allTypes = (from a in assemblies from t in a.GetTypes() select t).ToList();


            services.AddControllers();

            (
                from t in allTypes
                where typeof(IMappingModule).IsAssignableFrom(t) && !t.IsAbstract
                select (IMappingModule?)Activator.CreateInstance(t)
            ).Each(module => module.RegisterMappings(configuration));


            (
                from t in allTypes
                where typeof(IServicesModule).IsAssignableFrom(t) && !t.IsAbstract
                select (IServicesModule?)Activator.CreateInstance(t)
            ).Each(module => module.RegisterServices(services, configuration));


            services.AddMediatR(cfg =>
            {
                var mediatrModules = (
                    from t in allTypes
                    where typeof(IMediatrModule).IsAssignableFrom(t) && !t.IsAbstract
                    select (IMediatrModule?)Activator.CreateInstance(t)
                );
                if (mediatrModules.Any())
                    mediatrModules.Each(module => module.RegisterMediatrHandlers(cfg, configuration));
            });

            (
                from t in allTypes
                where typeof(IRebusModule).IsAssignableFrom(t) && !t.IsAbstract
                select (IRebusModule?)Activator.CreateInstance(t)
            ).Each(module => module.RegisterRebusHandlers(services, configuration));
        }

        public static void AddModularizer(this IServiceCollection services, IConfiguration configuration)
        {
            var entry = Assembly.GetEntryAssembly();
            if (entry is not null)
            {
                var assemblies = Directory.GetFiles(Path.GetDirectoryName(entry.Location)!)
                                          .Where(f => f.EndsWith("dll") && f.Contains("Modules"))
                                          .Select(e => Assembly.LoadFrom(e));
                services.AddModularizer(configuration, assemblies.ToArray());
            }
        }
    }
}

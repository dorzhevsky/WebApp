using Modularize;
using Rebus.Config;
using Rebus.Serialization.Json;

namespace WebApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IEnumerable<IConfigurationModule?> _modules;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ??
               throw new ArgumentNullException(nameof(configuration));

            _modules = Modularizer.LoadFromConfiguration(_configuration).ToList();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder builder = services.AddControllers();

            _modules.Each(module => module.RegisterControllers(builder, _configuration));

            services.AddMediatR(cfg =>
            {
                _modules.Each(module => module.RegisterMediatrHandlers(cfg, _configuration));
            });

            _modules.Each(module => module.RegisterServices(services, _configuration));
            _modules.Each(module => module.RegisterRebusHandlers(services, _configuration));
            _modules.Each(module => module.RegisterMappings(_configuration));

            services.AddRebus((configurer, _) => configurer
                            .Transport(t => t.UseRabbitMq("amqp://garda:123asdZXC$@192.168.36.111:34607", "webapp")
                            .ExchangeNames(directExchangeName: "WebAppDirect", topicExchangeName: "WebAppTopic"))
                            .Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.FullTypeInformation))
                            .Options(o =>
                            {
                            }));
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            app.UseEndpoints(e => e.MapControllers());
        }
    }
}

using Modularize;
using Rebus.Config;
using Rebus.Serialization.Json;

namespace WebApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ??
               throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRebus((configurer, _) => configurer
                .Transport(t => t.UseRabbitMq("amqp://garda:123asdZXC$@192.168.36.111:34607", "webapp")
                .ExchangeNames(directExchangeName: "WebAppDirect", topicExchangeName: "WebAppTopic"))
                .Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.FullTypeInformation))
                .Options(o =>
                {
                }));

            services.AddModularizer(_configuration, 
                typeof(Users.Api.Module.Modules).Assembly,
                typeof(Users.Handlers.Module.Modules).Assembly,
                typeof(Users.Services.Module.Modules).Assembly,
                typeof(Users.Postgres.Module.Modules).Assembly,         
                
                typeof(External.Api.Module.Modules).Assembly,
                typeof(External.Handlers.Module.Modules).Assembly
            );
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            app.UseEndpoints(e => e.MapControllers());
        }
    }
}

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
            services.AddRebus((configurer, _) => 
            configurer.Transport(t => t.UseRabbitMq(_configuration.GetConnectionString("Rabbit"), "webapp")
                      .ExchangeNames(directExchangeName: "WebAppDirect", topicExchangeName: "WebAppTopic"))
                      .Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.FullTypeInformation))
                      .Options(o => {}));

            services.AddModularizer(_configuration, 
                typeof(Users.Api.Modularize.Modules).Assembly,
                typeof(Users.Handlers.Modularize.Modules).Assembly,
                typeof(Users.Services.Modularize.Modules).Assembly,
                typeof(Users.Postgres.Modularize.Modules).Assembly,                
                typeof(External.Api.Modularize.Modules).Assembly,
                typeof(External.Handlers.Modularize.Modules).Assembly
            );
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            app.UseEndpoints(e => e.MapControllers());
        }
    }
}

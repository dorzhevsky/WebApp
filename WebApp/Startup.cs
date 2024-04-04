using LoggerManager;
using Modularize;
using NLog;
using NLog.Extensions.Logging;
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

            LogManager.Configuration = new NLogLoggingConfiguration(configuration.GetSection("NLog"));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggingManager, LoggingManager>();
            //services.AddRebus((configurer, _) => 
            //configurer.Transport(t => t.UseRabbitMq(_configuration.GetConnectionString("Rabbit"), "webapp")
            //          .ExchangeNames(directExchangeName: "WebAppDirect", topicExchangeName: "WebAppTopic"))
            //          .Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.FullTypeInformation))
            //          .Options(o => {}));

            services.AddModularizer(_configuration, 
                typeof(Users.Adapter.Api.Modularize.Modules).Assembly,                
                typeof(Users.Core.Services.Modularize.Modules).Assembly,
                typeof(Users.Adapter.Handlers.Modularize.Modules).Assembly,
                typeof(Users.Adapter.Postgres.Modularize.Modules).Assembly,                
                typeof(External.Adapter.Api.Modularize.Modules).Assembly,
                typeof(External.Adapter.Handlers.Modularize.Modules).Assembly
            );
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            app.UseEndpoints(e => e.MapControllers());
        }
    }
}

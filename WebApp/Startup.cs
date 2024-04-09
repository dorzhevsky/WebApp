using NLog;
using NLog.Extensions.Logging;
using Shared.LoggerManager;
using Shared.Modularize;

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

            // pathToBinDebug = Weather.Api/bin/Debug/netcoreapp3.1

            services.AddModularizer(_configuration);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            app.UseEndpoints(e => e.MapControllers());
        }
    }
}

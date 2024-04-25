using NLog;
using NLog.Extensions.Logging;
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
            //services.AddRebus((configurer, _) => 
            //configurer.Transport(t => t.UseRabbitMq(_configuration.GetConnectionString("Rabbit"), "webapp")
            //          .ExchangeNames(directExchangeName: "WebAppDirect", topicExchangeName: "WebAppTopic"))
            //          .Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.FullTypeInformation))
            //          .Options(o => {}));

            services.AddModularizer(_configuration);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ResponseLoggingMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseRouting();            
            app.UseEndpoints(e => e.MapControllers());            
        }
    }
}

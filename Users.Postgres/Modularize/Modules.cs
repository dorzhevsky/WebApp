using LinqToDB.AspNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularize;
using LinqToDB;

namespace Users.Postgres.Modularize
{
    public class Modules
    {
        public class ServicesModule : IServicesModule
        {
            public void RegisterServices(IServiceCollection services, IConfiguration configuration)
            {
                services.AddLinqToDBContext<UsersPostgresConnection>((provider, options) => options
                                .UsePostgreSQL(configuration.GetConnectionString("Garda"))
                                .UseMappingSchema(Schema.GetSchema()));
            }
        }
    }
}

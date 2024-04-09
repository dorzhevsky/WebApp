using LinqToDB.AspNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LinqToDB;
using Shared.Modularize;

namespace Modules.Users.Adapter.Postgres.Modularize
{
    public class Modules
    {
        public class ServicesModule : IServicesModule
        {
            public void RegisterServices(IServiceCollection services, IConfiguration configuration)
            {
                services.AddLinqToDBContext<UsersPostgresConnection>((provider, options) => options
                                .UsePostgreSQL(configuration.GetConnectionString("Garda"))
                                .UseMappingSchema(Schema.Schema.GetSchema()));
            }
        }
    }
}

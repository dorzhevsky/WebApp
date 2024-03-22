using LinqToDB.AspNet;
using Microsoft.Extensions.DependencyInjection;
using LinqToDB;
using Microsoft.Extensions.Configuration;

namespace Users.Postgres
{
    public static class Setup
    {
        public static void RegisterPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLinqToDBContext<UsersPostgresConnection>((provider, options) => options
                            .UsePostgreSQL(configuration.GetConnectionString("Garda"))
                            .UseMappingSchema(Schema.GetSchema()));
        }
    }
}

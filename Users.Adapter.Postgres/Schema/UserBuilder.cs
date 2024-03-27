using LinqToDB;
using LinqToDB.Mapping;
using Users.Core.Domain;

namespace Users.Adapter.Postgres.Schema
{
    internal static class UserBuilder
    {
        public static void ConfigureUser(this FluentMappingBuilder fluentMappingBuilder)
        {
            var builder = fluentMappingBuilder.Entity<User>();
            builder.HasTableName("test_user")
                   .HasSchemaName("public")
                   .Property(x => x.Id).HasColumnName("id").IsPrimaryKey().IsIdentity()
                   .Property(x => x.Name).HasColumnName("name").HasDataType(DataType.VarChar);
        }
    }
}

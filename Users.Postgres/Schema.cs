using LinqToDB;
using LinqToDB.Mapping;
using Users.Domain;

namespace Users.Postgres
{
    internal static class Schema
    {

        public static MappingSchema GetSchema()
        {
            var mapping = new MappingSchema();
            var fluentMappingBuilder = new FluentMappingBuilder(mapping);

            User(fluentMappingBuilder);

            fluentMappingBuilder.Build();
            return mapping;
        }

        private static void User(FluentMappingBuilder fluentMappingBuilder)
        {
            var builder = fluentMappingBuilder.Entity<User>();
            builder.HasTableName("test_user")
                   .HasSchemaName("public")
                   .Property(x => x.Id).HasColumnName("id").IsPrimaryKey().IsIdentity()
                   .Property(x => x.Name).HasColumnName("name").HasDataType(DataType.VarChar);
        }
    }
}

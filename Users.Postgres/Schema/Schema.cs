using LinqToDB.Mapping;

namespace Users.Adapter.Postgres.Schema
{
    internal static class Schema
    {
        public static MappingSchema GetSchema()
        {
            var mapping = new MappingSchema();
            var fluentMappingBuilder = new FluentMappingBuilder(mapping);

            fluentMappingBuilder.ConfigureUser();

            fluentMappingBuilder.Build();
            return mapping;
        }
    }
}

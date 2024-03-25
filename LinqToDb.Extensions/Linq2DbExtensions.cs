using LinqToDB;

namespace LinqToDb.Extensions
{
    public static class Linq2DbExtensions
    {
        public static void RecreateTables(this IDataContext connection)
        {
            var types = connection.MappingSchema.GetDefinedTypes();

            void CreateTable(Type type)
            {
                var method = typeof(DataExtensions).GetMethod(nameof(DataExtensions.CreateTable), System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                var closedMethod = method.MakeGenericMethod(type);
                closedMethod.Invoke(null, new object[]
                {   connection,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing ,
                    Type.Missing,
                    Type.Missing
                });
            }

            void DropTable(Type type)
            {
                var methods = typeof(DataExtensions).GetMethods();
                var method = methods.FirstOrDefault(e => e.Name == (nameof(DataExtensions.DropTable)) && e.GetParameters().Any(p => p.ParameterType == typeof(IDataContext)));
                var closedMethod = method.MakeGenericMethod(type);
                closedMethod.Invoke(null, new object[]
                {   connection,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing,
                    false,
                    Type.Missing,
                    Type.Missing
                });
            }

            foreach (var type in types)
            {
                DropTable(type);
                CreateTable(type);
            }
        }
    }
}
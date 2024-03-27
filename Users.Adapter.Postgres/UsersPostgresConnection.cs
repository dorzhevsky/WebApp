using LinqToDB;
using LinqToDB.Data;

namespace Users.Adapter.Postgres
{
    public class UsersPostgresConnection : DataConnection
    {
        public UsersPostgresConnection(DataOptions opts) : base(opts)
        {

        }
    }
}
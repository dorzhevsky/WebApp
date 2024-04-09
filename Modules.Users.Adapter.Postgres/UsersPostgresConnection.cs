using LinqToDB;
using LinqToDB.Data;

namespace Modules.Users.Adapter.Postgres
{
    public class UsersPostgresConnection : DataConnection
    {
        public UsersPostgresConnection(DataOptions opts) : base(opts)
        {

        }
    }
}
using LinqToDB;
using LinqToDB.Data;

namespace Users.Postgres
{
    public class UsersPostgresConnection : DataConnection
    {
        public UsersPostgresConnection(DataOptions opts) : base(opts)
        {

        }
    }
}
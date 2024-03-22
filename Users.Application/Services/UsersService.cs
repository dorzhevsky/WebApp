using Contracts;
using LinqToDB;
using MediatR;
using Users.Application.Interfaces;
using Users.Contracts;
using Users.Domain;
using Users.Postgres;

namespace Users.Application.Services
{
    internal class UsersService : IUsersService, IService
    {
        private readonly IMediator _bus;
        private readonly UsersPostgresConnection _postgresConnection;

        public UsersService(IMediator bus, UsersPostgresConnection usersPostgresConnection)
        {
            _bus = bus;
            _postgresConnection = usersPostgresConnection;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var data = await _postgresConnection.GetTable<User>().ToListAsync();
            return data;
        }

        public async Task<string> GetDataFromExternalModule()
        {
            var data = await _bus.Send(new GetExternalData());
            return data;
        }

        public async Task SendNotification()
        {
            await _bus.Publish(new UserDeletedNotification());
        }

        public void Update()
        {
            _postgresConnection.GetTable<User>().Set(f => f.Name, f => f.Name.ToUpper()).Update();
        }
    }
}
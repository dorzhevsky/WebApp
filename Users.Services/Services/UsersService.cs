using Contracts.Modules.External;
using Contracts.Modules.Users;
using LinqToDB;
using MediatR;
using Users.Domain;
using Users.Postgres;
using Users.Services.Interfaces;
using Users.Services.Messages;

namespace Users.Services.Services
{
    internal class UsersService : IUsersService, IService, INotificationHandler<UpdateUsers>
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

        public async Task Handle(UpdateUsers notification, CancellationToken cancellationToken)
        {
            _postgresConnection.GetTable<User>()
                               .Set(f => f.Name, f => f.Name.ToUpper())
                               .Update();
        }
    }
}
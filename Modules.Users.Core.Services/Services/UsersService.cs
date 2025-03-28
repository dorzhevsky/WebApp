﻿using Domain.Contracts.Modules.External;
using Domain.Contracts.Modules.Users;
using LinqToDB;
using MediatR;
using Modules.Users.Adapter.Postgres;
using Modules.Users.Core.Domain;
using Modules.Users.Core.Services.Interfaces;
using Modules.Users.Core.Services.Messages;

namespace Modules.Users.Core.Services.Services
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
using NUnit.Framework;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Snapper.Nunit;
using LinqToDB;
using Shared.LinqToDb.Extensions;
using Modules.Users.Adapter.Postgres;
using Modules.Users.Core.Domain;
using Domain.Contracts.Modules.Users;

namespace Modules.Users.Adapter.Handlers.Tests
{
    public class UsersCommandHandlerTest
    {
        [Test]
        public async Task Test1()
        {
            var serviceProvider = Setup.Init(services => { });
            var db = serviceProvider.GetService<UsersPostgresConnection>();
            db.RecreateTables();
            db.Insert(new User { Id = 1, Name = "test" });

            var mediator = serviceProvider.GetService<IMediator>();
            var users = await mediator.Send(new GetUsersCommand());

            Assert.That(users, Matches.Snapshot());
        }
    }
}
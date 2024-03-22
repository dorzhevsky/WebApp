using NUnit.Framework;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Snapper.Nunit;
using LinqToDb.Extensions;
using LinqToDB;
using Users.Postgres;
using Users.Domain;
using Users.Contracts;

namespace Users.Handlers.Tests
{
    public class UsersCommandHandlerTest
    {
        [Test]
        public async Task Test1()
        {
            var serviceProvider = Setup.Init(services =>
            {
                services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(ExternalNotificationdHandler).Assembly);
                });
            });
            var db = serviceProvider.GetService<UsersPostgresConnection>();
            db.DropCreateTables();
            db.Insert(new User { Id = 1, Name = "test" });

            var mediator = serviceProvider.GetService<IMediator>();
            var users = await mediator.Send(new GetUsersCommand());

            Assert.That(users, Matches.Snapshot());
        }
    }
}
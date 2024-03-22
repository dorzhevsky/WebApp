using NUnit.Framework;
using Contracts;
using LinqToDB;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Snapper.Nunit;
using LinqToDb.Extensions;
using Users.Postgres;
using Users.Domain;
using Users.Api.Users;
using Users.Contracts;

namespace Tests.Users.Api
{
    public class UsersControllerTest
    {
        [Test]
        public async Task Test1()
        {
            var serviceProvider = Setup.Init(services =>
            {
                var mediator = new Mock<IMediator>();
                mediator
                    .Setup(i => i.Send(It.IsAny<GetExternalData>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult("Some data"));
                services.AddSingleton(s => mediator.Object);

                services.AddTransient<UsersController>();
            });
            var controller = serviceProvider.GetService<UsersController>();
            IActionResult data = await controller.GetDataFromExternalModule();
            var jsonResult = data as JsonResult;
            Assert.That(jsonResult, Is.Not.Null);
            Assert.That(jsonResult.Value, Is.EqualTo("Some data"));
        }

        [Test]
        public async Task Test2()
        {
            var serviceProvider = Setup.Init(services =>
            {
                var mediator = new Mock<IMediator>();
                services.AddSingleton(s => mediator.Object);
                services.AddTransient<UsersController>();
            });
            var db = serviceProvider.GetService<UsersPostgresConnection>();
            db.DropCreateTables();
            db.Insert(new User { Id = 1, Name = "test1" });

            var controller = serviceProvider.GetService<UsersController>();
            IActionResult data = await controller.GetDataFromDatabase();
            var jsonResult = data as JsonResult;
            Assert.That(jsonResult, Is.Not.Null);
            Assert.That(jsonResult.Value, Matches.Snapshot());
        }


        [Test]
        public async Task Test3()
        {
            var serviceProvider = Setup.Init(services =>
            {
                var mediator = new Mock<IMediator>();
                services.AddSingleton(s => mediator.Object);
                services.AddTransient<UsersController>();
            });
            var db = serviceProvider.GetService<UsersPostgresConnection>();
            db.DropCreateTables();
            db.Insert(new User { Id = 1, Name = "test" });

            var controller = serviceProvider.GetService<UsersController>();
            IActionResult data = await controller.GetDataFromDatabase();
            var jsonResult = data as JsonResult;
            Assert.That(jsonResult, Is.Not.Null);
            Assert.That(jsonResult.Value, Matches.Snapshot());
        }

        [Test]
        public async Task Test4()
        {
            UserDeletedNotification notif = null;
            var serviceProvider = Setup.Init(services =>
            {
                var mediator = new Mock<IMediator>();
                mediator.Setup(m => m.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()))
               .Callback<object, CancellationToken>((notification, cToken) =>
               {
                   notif = (UserDeletedNotification)notification;
               });
                services.AddSingleton(s => mediator.Object);

                services.AddTransient<UsersController>();
            });

            var controller = serviceProvider.GetService<UsersController>();
            IActionResult data = await controller.SendNotification();

            Assert.That(notif, Matches.Snapshot());
        }
    }
}
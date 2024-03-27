﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularize;
using Users.Adapter.Api.Users;

namespace Users.Adapter.Api.Modularize
{
    public class Modules
    {
        public class ControllersModule : IControllersModule
        {
            public void RegisterControllers(IMvcBuilder builder, IConfiguration _configuration)
            {
                builder.AddApplicationPart(typeof(UsersController).Assembly);
            }
        }
    }
}

﻿namespace Modules.Users.Core.Services.Interfaces
{
    public interface IUsersHostedService
    {
        void Tell(object message);
    }
}

﻿using Users.Domain;

namespace Users.Services.Interfaces
{
    public interface IUsersService
    {
        Task<string> GetDataFromExternalModule();
        Task<IEnumerable<User>> GetUsers();
        Task SendNotification();
    }
}

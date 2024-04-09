using Modules.Users.Core.Domain;

namespace Modules.Users.Core.Services.Interfaces
{
    public interface IUsersService
    {
        Task<string> GetDataFromExternalModule();
        Task<IEnumerable<User>> GetUsers();
        Task SendNotification();
    }
}

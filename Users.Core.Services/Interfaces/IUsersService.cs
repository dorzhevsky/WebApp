using Users.Core.Domain;

namespace Users.Core.Services.Interfaces
{
    public interface IUsersService
    {
        Task<string> GetDataFromExternalModule();
        Task<IEnumerable<User>> GetUsers();
        Task SendNotification();
    }
}

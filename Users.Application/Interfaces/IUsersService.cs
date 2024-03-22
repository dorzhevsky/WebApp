using Users.Domain;

namespace Users.Application.Interfaces
{
    public interface IUsersService
    {
        Task<string> GetDataFromExternalModule();
        Task<IEnumerable<User>> GetUsers();
        void Update();
        Task SendNotification();
    }
}

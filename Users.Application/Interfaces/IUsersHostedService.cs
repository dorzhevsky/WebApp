namespace Users.Services.Interfaces
{
    public interface IUsersHostedService
    {
        void Tell(object message);
    }
}

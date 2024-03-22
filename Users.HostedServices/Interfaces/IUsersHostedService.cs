namespace Users.HostedServices.Interfaces
{
    public interface IUsersHostedService
    {
        void Tell(object message);
    }
}

using MediatR;

namespace Contracts.Modules.Users
{
    public class GetUsersCommand : IRequest<IEnumerable<UserDto>>
    {
    }
}

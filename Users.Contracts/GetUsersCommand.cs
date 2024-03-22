using MediatR;

namespace Users.Contracts
{
    public class GetUsersCommand : IRequest<IEnumerable<UserDto>>
    {
    }
}

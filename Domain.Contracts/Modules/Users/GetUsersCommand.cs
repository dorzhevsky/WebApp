using MediatR;

namespace Domain.Contracts.Modules.Users
{
    public class GetUsersCommand : IRequest<IEnumerable<UserDto>>
    {
    }
}

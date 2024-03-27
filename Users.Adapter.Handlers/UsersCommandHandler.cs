using Contracts.Modules.Users;
using MediatR;
using Nelibur.ObjectMapper;
using Users.Core.Services.Interfaces;

namespace Users.Adapter.Handlers
{
    public class UsersCommandHandler : IRequestHandler<GetUsersCommand, IEnumerable<UserDto>>
    {
        private readonly IUsersService _service;

        public UsersCommandHandler(IUsersService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
        {
            var users = await _service.GetUsers();
            return users.Select(u => TinyMapper.Map<UserDto>(u));
        }
    }
}
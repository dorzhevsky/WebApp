using Domain.Contracts.Modules.External;
using MediatR;

namespace Modules.External.Adapter.Handlers
{
    public class GetExternalDataHandler : IRequestHandler<GetExternalData, string>
    {
        public Task<string> Handle(GetExternalData request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Hello from GetDataCommandHandler");
        }
    }
}
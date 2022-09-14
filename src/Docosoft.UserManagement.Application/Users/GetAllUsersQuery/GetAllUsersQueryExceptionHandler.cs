using Docosoft.UserManagement.Application.SeedWork;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Docosoft.UserManagement.Application.Users
{
    public class GetAllUsersQueryExceptionHandler : IRequestExceptionHandler<GetAllUsersQuery, IList<UserDto>, Exception>
    {
        // TODO: Extract common logic to Base class
        private readonly ILogger<GetAllUsersQueryExceptionHandler> _logger;

        public GetAllUsersQueryExceptionHandler(ILogger<GetAllUsersQueryExceptionHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(GetAllUsersQuery request, Exception exception, RequestExceptionHandlerState<IList<UserDto>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");
            state.SetHandled(null);
            return Task.CompletedTask;
        }
    }
}
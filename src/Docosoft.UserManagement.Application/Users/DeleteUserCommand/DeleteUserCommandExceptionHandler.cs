using Docosoft.UserManagement.Application.SeedWork;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Docosoft.UserManagement.Application.Users
{
    public class DeleteUserCommandExceptionHandler : IRequestExceptionHandler<DeleteUserCommand, ResponseDto<UserDto>, Exception>
    {
        // TODO: Extract common logic to Base class
        private readonly ILogger<DeleteUserCommandExceptionHandler> _logger;

        public DeleteUserCommandExceptionHandler(ILogger<DeleteUserCommandExceptionHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DeleteUserCommand request, Exception exception, RequestExceptionHandlerState<ResponseDto<UserDto>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            var response = new ResponseDto<UserDto>(null, true, exception);
            response.Message = exception.Message;

            state.SetHandled(response);

            return Task.CompletedTask;
        }
    }
}
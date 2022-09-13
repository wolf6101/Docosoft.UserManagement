using Docosoft.UserManagement.Application.SeedWork;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Docosoft.UserManagement.Application.Users
{
    public class CreateUserCommandExceptionHandler : IRequestExceptionHandler<CreateUserCommand, ResponseDto<UserDto>, Exception>
    {
        private readonly ILogger<CreateUserCommandExceptionHandler> _logger;

        public CreateUserCommandExceptionHandler(ILogger<CreateUserCommandExceptionHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CreateUserCommand request, Exception exception, RequestExceptionHandlerState<ResponseDto<UserDto>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            var response = new ResponseDto<UserDto>(null, true, exception);
            response.Message = exception.Message;

            state.SetHandled(response);
            return Task.CompletedTask;
        }
    }
}
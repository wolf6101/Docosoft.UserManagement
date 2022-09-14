using Docosoft.UserManagement.Application.SeedWork;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Docosoft.UserManagement.Application.Users
{
    public class UpdateUserCommandExceptionHandler : IRequestExceptionHandler<UpdateUserCommand, ResponseDto<UserDto>, Exception>
    {
        private readonly ILogger<UpdateUserCommandExceptionHandler> _logger;

        public UpdateUserCommandExceptionHandler(ILogger<UpdateUserCommandExceptionHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UpdateUserCommand request, Exception exception, RequestExceptionHandlerState<ResponseDto<UserDto>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            var response = new ResponseDto<UserDto>(null, true, exception);
            response.ResourceCreated = false;
            response.ResourceUpdated = false;

            response.Message = exception.Message;

            state.SetHandled(response);
            return Task.CompletedTask;
        }
    }
}
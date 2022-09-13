using Docosoft.UserManagement.Application.SeedWork;
using Docosoft.UserManagement.Application.Users;

using MediatR.Pipeline;

public class CreateUserCommandExceptionHandler : IRequestExceptionHandler<CreateUserCommand, ResponseDto<UserDto>, Exception>
{
    public Task Handle(CreateUserCommand request, Exception exception, RequestExceptionHandlerState<ResponseDto<UserDto>> state, CancellationToken cancellationToken)
    {
        var response = new ResponseDto<UserDto>(null, true, exception);
        response.Message = exception.Message;

        state.SetHandled(response);
        return Task.CompletedTask;
    }
}
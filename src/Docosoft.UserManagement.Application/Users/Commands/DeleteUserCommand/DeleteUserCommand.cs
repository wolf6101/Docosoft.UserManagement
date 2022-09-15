using Docosoft.UserManagement.Application.SeedWork;

using MediatR;

namespace Docosoft.UserManagement.Application.Users
{
    public class DeleteUserCommand : IRequest<ResponseDto<UserDto>>
    {
        public DeleteUserCommand()
        {
        }

        public DeleteUserCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
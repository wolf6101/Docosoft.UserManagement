using Docosoft.UserManagement.Application.SeedWork;

using MediatR;

namespace Docosoft.UserManagement.Application.Users
{
    public class CreateUserCommand : IRequest<ResponseDto<UserDto>>
    {
        public CreateUserCommand(string firstName, string lastName, string gender, string email, Guid userRoleId)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.Email = email;
            this.UserRoleId = userRoleId;
        }
        public CreateUserCommand(CreateUserRequestDto request)
        {
            this.FirstName = request.FirstName;
            this.LastName = request.LastName;
            this.Gender = request.Gender;
            this.Email = request.Email;
            this.UserRoleId = request.UserRoleId;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Gender { get; }
        public string Email { get; }
        public Guid UserRoleId { get; }
    }
}
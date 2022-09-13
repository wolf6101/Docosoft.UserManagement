using Docosoft.UserManagement.Application.SeedWork;

using MediatR;

namespace Docosoft.UserManagement.Application.Users
{
    public class UpdateUserCommand : IRequest<ResponseDto<UserDto>>
    {
        public UpdateUserCommand(Guid id, string firstName, string lastName, string gender, string email, Guid userRoleId)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.Email = email;
            this.UserRoleId = userRoleId;
        }
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Gender { get; }
        public string Email { get; }
        public Guid UserRoleId { get; }
    }
}
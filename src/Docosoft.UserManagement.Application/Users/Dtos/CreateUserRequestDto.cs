using Docosoft.UserManagement.Application.SeedWork;

namespace Docosoft.UserManagement.Application.Users
{
    public class CreateUserRequestDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public Guid UserRoleId { get; set; }
    }
}
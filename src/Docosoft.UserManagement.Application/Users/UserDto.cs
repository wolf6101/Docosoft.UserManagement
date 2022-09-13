using Docosoft.UserManagement.Application.SeedWork;

namespace Docosoft.UserManagement.Application.Users
{
    public class UserDto : IDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string UserRoleName { get; set; }
    }
}
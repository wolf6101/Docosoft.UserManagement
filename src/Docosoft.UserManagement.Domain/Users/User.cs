using Docosoft.UserManagement.Domain.SeedWork;

namespace Docosoft.UserManagement.Domain.Users
{
    public class User : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public GenderEnum Gender { get; private set; }
        public string Email { get; private set; }
        public Guid UserRoleId {get; private set; }
        public Guid UserGroupId {get; private set; }
        public DateTime CreatedDateTime {get; private set;}
        public DateTime LastUpdatedDateTime {get; private set;}

        // Navigation Properties
        public IEnumerable<UserGroup> Groups {get; private set; }
        public UserRole Role {get; private set; }


    }
}
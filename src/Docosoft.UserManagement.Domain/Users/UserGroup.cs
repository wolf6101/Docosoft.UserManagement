using Docosoft.UserManagement.Domain.SeedWork;

namespace Docosoft.UserManagement.Domain.Users
{
    public class UserGroup : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        // Navigation Properties
        public IEnumerable<User> Users { get; private set; }
    }
}
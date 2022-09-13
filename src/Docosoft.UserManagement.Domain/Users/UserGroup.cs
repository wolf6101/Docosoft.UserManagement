using Docosoft.UserManagement.Domain.SeedWork;

namespace Docosoft.UserManagement.Domain.Users
{
    public class UserGroup : Entity
    {
        public UserGroup(Guid id, string name, string description) : base(id)
        {
            this.Name = name;
            this.Description = description;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }

        // Navigation Properties
        public IEnumerable<User> Users { get; private set; }
    }
}
using Docosoft.UserManagement.Domain.SeedWork;

namespace Docosoft.UserManagement.Domain.Users
{
    public class User : Entity
    {
        public User(Guid id, string firstName, string lastName, GenderEnum gender, string email, Guid userRoleId, DateTime createdDateTime, DateTime lastUpdatedDateTime)
        : base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.Email = email;
            this.UserRoleId = userRoleId;
            this.CreatedDateTime = createdDateTime;
            this.LastUpdatedDateTime = lastUpdatedDateTime;
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public GenderEnum Gender { get; private set; }
        public string Email { get; private set; }
        public Guid UserRoleId { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime LastUpdatedDateTime { get; private set; }

        // Navigation Properties
        public IEnumerable<UserGroup> Groups { get; private set; }
        public UserRole Role { get; private set; }

    }
}
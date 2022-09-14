using Docosoft.UserManagement.Domain.SeedWork;

namespace Docosoft.UserManagement.Domain.Users
{
    public class User : Entity
    {
        public User(Guid id, string firstName, string lastName, GenderEnum gender, string email, Guid userRoleId)
        : base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.Email = email;
            this.UserRoleId = userRoleId;
            
            CreatedDateTime = DateTime.Now;
            LastUpdatedDateTime = DateTime.Now;
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

        public void UpdateUser(User user)
        {
            if (!string.IsNullOrEmpty(user.FirstName)) FirstName = user.FirstName;
            if (!string.IsNullOrEmpty(user.LastName)) LastName = user.LastName;
            if (!string.IsNullOrEmpty(user.Email)) Email = user.Email;
            if (user.UserRoleId != null) UserRoleId = user.UserRoleId;

            Gender = user.Gender;
            LastUpdatedDateTime = DateTime.Now;
        }

        // This is responsibility of the client to make sure UserRole exists in the db, otherwise exception on dbcontext.SaveChanges
        public bool SetUserRole(UserRole role) {
            if (role.Id == Guid.Empty) return false;
            if (String.IsNullOrEmpty(role.Name)) return false;
            
            UserRoleId = role.Id;
            Role = role;

            return true;
        }
    }
}
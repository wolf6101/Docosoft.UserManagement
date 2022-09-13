namespace Docosoft.UserManagement.Domain.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetAsync(Guid id);

        Task<User> AddAsync(User user);

        Task<User> DeleteAsync(Guid id);

        Task<User> UpdateAsync(Guid id, User user);

        Task<IEnumerable<UserGroup>> GetUserGroups(Guid id);
    }
}
namespace Docosoft.UserManagement.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllAsync(int offset, int limit);

        Task<User?> GetAsync(Guid id);

        Task<User> AddAsync(User user);

        Task<User> DeleteAsync(Guid id);

        Task<User> UpdateAsync(Guid id, User user);

        Task<IEnumerable<UserGroup>> GetUserGroups(Guid id);
    }
}
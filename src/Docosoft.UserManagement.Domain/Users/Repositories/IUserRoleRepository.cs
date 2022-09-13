namespace Docosoft.UserManagement.Domain.Users.Repositories
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetAllAsync();

        Task<UserRole?> GetAsync(Guid id);
    }
}
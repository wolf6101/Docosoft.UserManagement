namespace Docosoft.UserManagement.Domain.Users
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetAllAsync();

        Task<UserRole?> GetAsync(Guid id);
    }
}
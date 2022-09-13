using Docosoft.UserManagement.Domain.Users;

using Microsoft.EntityFrameworkCore;

namespace Docosoft.UserManagement.Infrastructure.Domain.Users
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly UserContext _context;

        public UserRoleRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole?> GetAsync(Guid id)
        {
            return await _context.UserRoles.FirstOrDefaultAsync(ur => ur.Id == id);
        }
    }
}
using Docosoft.UserManagement.Domain.Users;
using Docosoft.UserManagement.Domain.Users.Repositories;
using Docosoft.UserManagement.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Docosoft.UserManagement.Infrastructure.Domain.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public async Task<User> AddAsync(User user)
        {
            var response = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return response.Entity;
        }

        public async Task<User?> DeleteAsync(User user)
        {
            if (user == null) return null;

            var response = _context.Remove(user);
            await _context.SaveChangesAsync();

            return response.Entity;
        }

        public async Task<IList<User>> GetAllAsync(int offset, int limit)
        {
            return await _context.Users.Include(u => u.Role)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<User?> GetAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<IEnumerable<UserGroup>> GetUserGroups(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> UpdateAsync(Guid id, User user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) return null;

            existingUser.UpdateUser(user);
            await _context.SaveChangesAsync();

            return existingUser; ;
        }
    }
}
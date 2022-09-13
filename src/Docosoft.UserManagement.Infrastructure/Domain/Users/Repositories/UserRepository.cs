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

        public Task<User> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
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

        public Task<User> UpdateAsync(Guid id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
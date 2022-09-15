using Docosoft.UserManagement.Domain.SeedWork;
using Docosoft.UserManagement.Domain.Users.Repositories;

namespace Docosoft.UserManagement.Domain.BusinessRules
{
    public class UserRoleShouldExist : IBusinessRule
    {
        private readonly Guid _roleId;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleShouldExist(Guid roleId, IUserRoleRepository userRoleRepository)
        {
            _roleId = roleId;
            _userRoleRepository = userRoleRepository;
        }

        public string Message => $"User role with Id: \"{_roleId}\" doesn't exist";

        public string Name => typeof(UserRoleShouldExist).Name;

        public async Task<bool> IsBroken()
        {
            var userRole = await _userRoleRepository.GetAsync(_roleId);

            return userRole == null;
        }
    }
}
using Docosoft.UserManagement.Application.SeedWork;
using Docosoft.UserManagement.Domain.BusinessRules;
using Docosoft.UserManagement.Domain.SeedWork;
using Docosoft.UserManagement.Domain.Users;
using Docosoft.UserManagement.Domain.Users.Repositories;

using MediatR;

namespace Docosoft.UserManagement.Application.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseDto<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IBusinessRuleValidator _validator;

        public UpdateUserCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IBusinessRuleValidator validator)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _validator = validator;
        }
        public async Task<ResponseDto<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var rules = new List<IBusinessRule> {
                new GenderShouldBeValid(request.Gender),
                new UserRoleShouldExist(request.UserRoleId, _userRoleRepository),
            };

            await _validator.AssertRules(rules);

            var gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), request.Gender, true);

            var user = new User(
                request.Id,
                request.FirstName,
                request.LastName,
                gender,
                request.Email,
                request.UserRoleId);

            var wasCreated = false;

            var response = await _userRepository.UpdateAsync(user.Id, user);

            if (response == null)
            {
                response = await _userRepository.AddAsync(user);
                wasCreated = true;
            }

            var userDto = new UserDto
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                Gender = response.Gender.ToString(),
                Email = response.Email,
                UserRoleName = response.Role.Name,
                UserRoleId = response.Role.Id
            };

            return new ResponseDto<UserDto>(userDto)
            {
                ResourceCreated = wasCreated,
                ResourceUpdated = !wasCreated,
            };
        }
    }
}
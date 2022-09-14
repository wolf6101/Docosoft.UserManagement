using Docosoft.UserManagement.Application.SeedWork;
using Docosoft.UserManagement.Domain.BusinessRules;
using Docosoft.UserManagement.Domain.SeedWork;
using Docosoft.UserManagement.Domain.Users;
using Docosoft.UserManagement.Domain.Users.Repositories;

using MediatR;

namespace Docosoft.UserManagement.Application.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseDto<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public CreateUserCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;

        }
        public async Task<ResponseDto<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var rules = new List<IBusinessRule> {
                    new GenderShouldBeValid(request.Gender),
                    new UserRoleShouldExist(request.UserRoleId, _userRoleRepository),
                };
            await new BusinessRuleValidator(rules).AssertRules();

            var gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), request.Gender, true);
            var user = new User(
                new Guid(),
                request.FirstName,
                request.LastName,
                gender,
                request.Email,
                request.UserRoleId);

            var response = await _userRepository.AddAsync(user);
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

            return new ResponseDto<UserDto>(userDto);
        }
    }
}
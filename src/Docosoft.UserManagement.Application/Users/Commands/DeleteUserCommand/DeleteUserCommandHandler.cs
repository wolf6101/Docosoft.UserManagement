using Docosoft.UserManagement.Application.SeedWork;
using Docosoft.UserManagement.Domain.Users.Repositories;

using MediatR;

namespace Docosoft.UserManagement.Application.Users
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseDto<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;

        }
        public async Task<ResponseDto<UserDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id);

            if (user == null) return new ResponseDto<UserDto>(null);

            if (user.Role.Name == "SuperAdmin")
            {
                return new ResponseDto<UserDto>(null, true, null) { Message = "You don't have enough permissions" };
            }

            var result = await _userRepository.DeleteAsync(user);

            return new ResponseDto<UserDto>(new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender.ToString(),
                Email = user.Email,
                UserRoleName = user.Role.Name,
                UserRoleId = user.Role.Id
            });
        }
    }
}
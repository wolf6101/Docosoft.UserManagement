using Docosoft.UserManagement.Domain.Users;
using Docosoft.UserManagement.Domain.Users.Repositories;

using MediatR;

namespace Docosoft.UserManagement.Application.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository repository)
        {
            _userRepository = repository;

        }
        public async Task<UserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId);

            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender.ToString(),
                Email = user.Email,
                UserRoleName = user.Role.Name,
                UserRoleId = user.Role.Id
            };
        }
    }
}
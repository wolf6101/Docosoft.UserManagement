using Docosoft.UserManagement.Domain.Users.Repositories;

using MediatR;

namespace Docosoft.UserManagement.Application.Users
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IList<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public async Task<IList<UserDto>?> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(request.Offset, request.Limit);

            if (users == null || users.Count == 0) return null;

            var result = new List<UserDto>();

            foreach (var user in users)
            {
                result.Add(new UserDto
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

            return result;
        }
    }
}
using MediatR;

namespace Docosoft.UserManagement.Application.Users
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
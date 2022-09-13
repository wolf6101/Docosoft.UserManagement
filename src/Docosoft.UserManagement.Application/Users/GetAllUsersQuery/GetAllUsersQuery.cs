using MediatR;

namespace Docosoft.UserManagement.Application.Users
{
    public class GetAllUsersQuery : IRequest<IList<UserDto>>
    {
        public GetAllUsersQuery()
        {
        }
        public GetAllUsersQuery(int offset, int limit)
        {
            this.Offset = offset;
            this.Limit = limit;
        }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
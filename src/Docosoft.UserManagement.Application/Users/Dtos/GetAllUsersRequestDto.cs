using Docosoft.UserManagement.Application.SeedWork;

namespace Docosoft.UserManagement.Application.Users
{
    public class GetAllUsersRequestDto : IDto
    {
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 100;

    }
}
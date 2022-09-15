using System;
using System.Threading;

using Docosoft.UserManagement.Application.Users;
using Docosoft.UserManagement.Domain.SeedWork;
using Docosoft.UserManagement.Domain.Users;
using Docosoft.UserManagement.Domain.Users.Repositories;

using Moq;

using Xunit;

namespace Docosoft.UserManagement.UnitTests.Application.Users.Commands
{
    public class CreateUserCommandTest
    {
        [Fact]
        public async void CreateUserCommandHandler_ValidCommand_ShouldResponseResponseDto()
        {
            // Arrange
            const string FirstName = "User First Name";
            const string LastName = "User Last Name";
            const string Email = "email@email.com";
            const string Gender = "Female";

            Guid UserRoleId = new Guid("0bf827f5-f9b6-49ff-90c0-c9563e24c022");

            var userRoleRepoMock = new Mock<IUserRoleRepository>();
            var userRole = new UserRole(UserRoleId, "TestRole", "Test Role");

            userRoleRepoMock.Setup(ur => ur.GetAsync(It.IsAny<Guid>())).ReturnsAsync(userRole);

            var userRepoMock = new Mock<IUserRepository>();
            var userId = new Guid("dec93d91-d4e6-4fa7-8cd3-b3a0f3e8a3f4");

            var user = new User(
                userId,
                FirstName,
                LastName,
                GenderEnum.Female,
                Email,
                UserRoleId);

            user.SetUserRole(userRole);

            userRepoMock.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(user);

            var validatorMock = new Mock<IBusinessRuleValidator>();

            var command = new CreateUserCommand(
                FirstName,
                LastName,
                Gender,
                Email,
                UserRoleId);

            var token = new CancellationToken();

            // Act
            var handler = new CreateUserCommandHandler(userRepoMock.Object, userRoleRepoMock.Object, validatorMock.Object);

            // Assert
            var result = await handler.Handle(command, token);

            Assert.NotNull(result.EntityDto);
            Assert.Equal(result.EntityDto.Id, userId);
            Assert.Equal(result.EntityDto.FirstName, FirstName);
            Assert.Equal(result.EntityDto.LastName, LastName);
            Assert.Equal(result.EntityDto.Email, Email);
            Assert.Equal(result.EntityDto.UserRoleId, UserRoleId);
            Assert.Equal(result.EntityDto.UserRoleName, userRole.Name);
        }
    }
}
using System;
using Xunit;
using Docosoft.UserManagement.Application.Users;
using Moq;
using Docosoft.UserManagement.Domain.Users.Repositories;
using Docosoft.UserManagement.Domain.Users;
using System.Threading;
using Docosoft.UserManagement.Domain.SeedWork;

namespace Docosoft.UserManagement.UnitTests.Application.Users.Commands
{
    public class DeleteUserCommandTest
    {
        [Fact]
        public async void DeleteUserCommandHandler_SuperAdmin_ShouldNotDelete()
        {
            // Arrange
            const string FirstName = "User First Name";
            const string LastName = "User Last Name";
            const string Email = "email@email.com";
            const string Gender = "Female";

            Guid UserRoleId = new Guid("0bf827f5-f9b6-49ff-90c0-c9563e24c022");

            var userRoleRepoMock = new Mock<IUserRoleRepository>();
            var userRole = new UserRole(UserRoleId, "SuperAdmin", "SuperAdmin Role");

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

            userRepoMock.Setup(r => r.GetAsync(userId)).ReturnsAsync(user);
            userRepoMock.Setup(r => r.DeleteAsync(It.IsAny<User>())).ReturnsAsync(user);

            var command = new DeleteUserCommand(userId);
            var token = new CancellationToken();

            // Act
            var handler = new DeleteUserCommandHandler(userRepoMock.Object, userRoleRepoMock.Object);

            // Assert
            var result = await handler.Handle(command, token);

            userRepoMock.Verify(r => r.GetAsync(userId), Times.AtLeastOnce());
            userRepoMock.Verify(r => r.DeleteAsync(It.IsAny<User>()), Times.Never());

            Assert.NotNull(result);
            Assert.True(result.ErrorOccured);
            Assert.Null(result.EntityDto);
        }

        [Fact]
        public async void DeleteUserCommandHandler_RegularUser_ShouldDelete()
        {
            // Arrange
            const string FirstName = "User First Name";
            const string LastName = "User Last Name";
            const string Email = "email@email.com";
            const string Gender = "Female";

            Guid UserRoleId = new Guid("0bf827f5-f9b6-49ff-90c0-c9563e24c022");

            var userRoleRepoMock = new Mock<IUserRoleRepository>();
            var userRole = new UserRole(UserRoleId, "TestRole", "Test Role");

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

            userRepoMock.Setup(r => r.GetAsync(userId)).ReturnsAsync(user);
            userRepoMock.Setup(r => r.DeleteAsync(It.IsAny<User>())).ReturnsAsync(user);

            var command = new DeleteUserCommand(userId);
            var token = new CancellationToken();

            // Act
            var handler = new DeleteUserCommandHandler(userRepoMock.Object, userRoleRepoMock.Object);

            // Assert
            var result = await handler.Handle(command, token);

            userRepoMock.Verify(r => r.GetAsync(userId), Times.AtLeastOnce());
            userRepoMock.Verify(r => r.DeleteAsync(It.IsAny<User>()), Times.AtLeastOnce());

            Assert.NotNull(result);
            Assert.False(result.ErrorOccured);
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
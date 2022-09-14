using System;
using Xunit;
using Docosoft.UserManagement.Application.Users;
using Moq;
using Docosoft.UserManagement.Domain.Users.Repositories;
using Docosoft.UserManagement.Domain.Users;
using System.Threading;
using System.Threading.Tasks;
using Docosoft.UserManagement.Domain.SeedWork;
using Docosoft.UserManagement.Domain.BusinessRules;

namespace Docosoft.UserManagement.IntegrationTests.Application.Users.Commands
{
    public class CreateUserCommandTest
    {
        [Fact]
        public async void CreateUserCommandHandler_ValidCommand_ShouldReturnResponseDto()
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

            var command = new CreateUserCommand(
                FirstName,
                LastName,
                Gender,
                Email,
                UserRoleId);

            var token = new CancellationToken();

            var validator = new BusinessRuleValidator();

            // Act
            var handler = new CreateUserCommandHandler(userRepoMock.Object, userRoleRepoMock.Object, validator);

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

        [Fact]
        public async void CreateUserCommandHandler_InvalidGender_ShouldThrowValidationException()
        {
            // Arrange
            const string FirstName = "User First Name";
            const string LastName = "User Last Name";
            const string Email = "email@email.com";
            const string Gender = "INVALID";

            Guid UserRoleId = new Guid("0bf827f5-f9b6-49ff-90c0-c9563e24c022");

            var userRoleRepoMock = new Mock<IUserRoleRepository>();
            var userRole = new UserRole(UserRoleId, "TestRole", "Test Role");

            userRoleRepoMock.Setup(ur => ur.GetAsync(It.IsAny<Guid>())).ReturnsAsync(userRole);

            var userRepoMock = new Mock<IUserRepository>();

            var command = new CreateUserCommand(
                FirstName,
                LastName,
                Gender,
                Email,
                UserRoleId);

            var token = new CancellationToken();

            var validator = new BusinessRuleValidator();

            // Act
            var handler = new CreateUserCommandHandler(userRepoMock.Object, userRoleRepoMock.Object, validator);

            // Assert
            var exception = await Assert.ThrowsAsync<BusinessRuleValidationException>(() => handler.Handle(command, token));
            Assert.Equal(exception.BrokenRule.Name, typeof(GenderShouldBeValid).Name);
        }

        [Fact]
        public async void CreateUserCommandHandler_RoleDoesntExist_ShouldThrowValidationException()
        {
            // Arrange
            const string FirstName = "User First Name";
            const string LastName = "User Last Name";
            const string Email = "email@email.com";
            const string Gender = "Male";

            Guid UserRoleId = new Guid("0bf827f5-f9b6-49ff-90c0-c9563e24c022");

            var userRoleRepoMock = new Mock<IUserRoleRepository>();

            userRoleRepoMock.Setup(ur => ur.GetAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            var userRepoMock = new Mock<IUserRepository>();

            var command = new CreateUserCommand(
                FirstName,
                LastName,
                Gender,
                Email,
                UserRoleId);

            var token = new CancellationToken();

            var validator = new BusinessRuleValidator();

            // Act
            var handler = new CreateUserCommandHandler(userRepoMock.Object, userRoleRepoMock.Object, validator);

            // Assert
            var exception = await Assert.ThrowsAsync<BusinessRuleValidationException>(() => handler.Handle(command, token));
            Assert.Equal(exception.BrokenRule.Name, typeof(UserRoleShouldExist).Name);
        }
    }
}
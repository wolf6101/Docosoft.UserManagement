using System;

using Docosoft.UserManagement.Domain.BusinessRules;
using Docosoft.UserManagement.Domain.Users;
using Docosoft.UserManagement.Domain.Users.Repositories;

using Moq;

using Xunit;

namespace Docosoft.UserManagement.UnitTests.Domain.BusinessRules
{
    public class UserRoleShouldExistTest
    {
        [Fact]
        public async void UserRoleShouldExist_RoleExists_ShouldReturnFalse()
        {
            // Arrange
            var id = new Guid("4ea82454-7296-4afb-9e05-09fc3e05fe38");
            var userRole = new UserRole(id, "Test Role", "Test Role");

            var repositoryMock = new Mock<IUserRoleRepository>();
            repositoryMock.Setup(r => r.GetAsync(id)).ReturnsAsync(userRole);

            var rule = new UserRoleShouldExist(id, repositoryMock.Object);

            // Act
            var result = await rule.IsBroken();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void UserRoleShouldExist_RoleDoesntExist_ShouldReturnTrue()
        {
            // Arrange
            var id = new Guid("a96f4ede-7130-48a7-9b8b-c24af41e1321");

            var repositoryMock = new Mock<IUserRoleRepository>();
            repositoryMock.Setup(r => r.GetAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            var rule = new UserRoleShouldExist(id, repositoryMock.Object);

            // Act
            var result = await rule.IsBroken();

            // Assert
            Assert.True(result);
        }

    }
}
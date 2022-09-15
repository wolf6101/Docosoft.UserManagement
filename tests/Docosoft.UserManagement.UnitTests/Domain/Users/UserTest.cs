using System;
using System.Collections.Generic;

using Docosoft.UserManagement.Domain.Users;

using Xunit;

namespace Docosoft.UserManagement.UnitTests.Domain.Users
{
    public class UserTest
    {
        [Fact]
        public void User_UpdateUser_ShouldNotOverrideUserWithEmptyValues()
        {
            // Arrange
            var userId = new Guid("4ea82454-7296-4afb-9e05-09fc3e05fe38");

            const string FirstName = "User First Name";
            const string LastName = "User Last Name";
            const string Email = "email@email.com";

            Guid UserRoleId = new Guid("0bf827f5-f9b6-49ff-90c0-c9563e24c022");

            var user = new User(
                userId,
                FirstName,
                LastName,
                GenderEnum.Female,
                Email,
                UserRoleId);

            var modifiedUser = new User(
                userId,
                "",
                "",
                GenderEnum.Unknown,
                Email,
                UserRoleId
            );
            // Act
            user.UpdateUser(modifiedUser);

            // Assert
            Assert.Equal(user.Id, userId);
            Assert.NotEqual(user.FirstName, modifiedUser.FirstName);
            Assert.NotEqual(user.LastName, modifiedUser.LastName);
            Assert.Equal(user.Gender, modifiedUser.Gender);
            Assert.Equal(user.Email, modifiedUser.Email);
            Assert.Equal(user.UserRoleId, modifiedUser.UserRoleId);
        }

        [Theory]
        [MemberData(nameof(InvalidTestUserRoles))]
        public void User_SetUserRole_InvalidRole_ShouldReturnFalse(UserRole userRole)
        {
            // Arrange
            var userId = new Guid("4ea82454-7296-4afb-9e05-09fc3e05fe38");

            const string FirstName = "User First Name";
            const string LastName = "User Last Name";
            const string Email = "email@email.com";

            Guid UserRoleId = new Guid("0bf827f5-f9b6-49ff-90c0-c9563e24c022");

            var user = new User(
                userId,
                FirstName,
                LastName,
                GenderEnum.Female,
                Email,
                UserRoleId);

            // Act
            var result = user.SetUserRole(userRole);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void User_SetUserRole_ValidRole_ShouldReturnTrue()
        {
            // Arrange
            var userId = new Guid("4ea82454-7296-4afb-9e05-09fc3e05fe38");

            const string FirstName = "User First Name";
            const string LastName = "User Last Name";
            const string Email = "email@email.com";

            Guid UserRoleId = new Guid("0bf827f5-f9b6-49ff-90c0-c9563e24c022");

            var user = new User(
                userId,
                FirstName,
                LastName,
                GenderEnum.Female,
                Email,
                UserRoleId);

            var userRole = new UserRole(new Guid("104b8774-d645-4977-82ee-1f59e1272ef8"), "Test Role", "Description");

            // Act
            var result = user.SetUserRole(userRole);

            // Assert
            Assert.True(result);
        }

        public static IEnumerable<object[]> InvalidTestUserRoles()
        {
            return new List<object[]>() {
                new object[] { new UserRole(Guid.Empty, "", "")},
                new object[] { new UserRole(new Guid("f5337e19-c1e4-4d9f-a17a-f9e632ef3d13"), "", "")}
            };
        }
    }
}
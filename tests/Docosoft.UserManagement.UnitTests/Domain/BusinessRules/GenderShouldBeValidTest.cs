using Docosoft.UserManagement.Domain.BusinessRules;

using Xunit;

namespace Docosoft.UserManagement.UnitTests.Domain.BusinessRules
{
    public class GenderShouldBeValidTest
    {
        [Theory]
        [InlineData("Male")]
        [InlineData("female")]
        [InlineData("unKNown")]
        [InlineData("Nonbinary")]
        public async void GenderShouldBeValid_ValidInput_ShouldReturnFalse(string gender)
        {
            // Arrange
            var rule = new GenderShouldBeValid(gender);
            // Act
            var result = await rule.IsBroken();
            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("fem")]
        [InlineData("Non Binary")]
        public async void GenderShouldBeValid_InvalidInput_ShouldReturnTrue(string gender)
        {
            // Arrange
            var rule = new GenderShouldBeValid(gender);
            // Act
            var result = await rule.IsBroken();
            // Assert
            Assert.True(result);
        }
    }
}
using Docosoft.UserManagement.Domain.SeedWork;
using Docosoft.UserManagement.Domain.Users;

namespace Docosoft.UserManagement.Domain.BusinessRules
{
    public class GenderShouldBeValid : IBusinessRule
    {
        private string _gender;

        public GenderShouldBeValid(string gender)
        {
            _gender = gender;
        }
        public string Message => $"gender: {_gender} is not valid. Possible values: Unknown | Male | Female | NonBinary";

        public string Name => typeof(GenderShouldBeValid).Name;

        public async Task<bool> IsBroken()
        {
            var isParsed = Enum.TryParse(typeof(GenderEnum), _gender, true, out var gender);

            return !isParsed;
        }
    }
}
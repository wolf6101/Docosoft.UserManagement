using Docosoft.UserManagement.Domain.SeedWork;
using Docosoft.UserManagement.Domain.Users;

namespace Docosoft.UserManagement.Domain.BusinessRules
{
    public class GenderShouldBeValid : IBusinessRule
    {
        private readonly string _gender;

        public GenderShouldBeValid(string gender)
        {
            _gender = gender;
        }
        public string Message
        {
            get
            {
                var possibleValues = Enum.GetValues(typeof(GenderEnum)).Cast<GenderEnum>().ToList();
                return $"gender: {_gender} is not valid. Possible values: {String.Join(" | ", possibleValues)}";
            }
        }

        public string Name => typeof(GenderShouldBeValid).Name;

        public Task<bool> IsBroken()
        {
            var isParsed = Enum.TryParse(typeof(GenderEnum), _gender, true, out var gender);

            return Task.FromResult(!isParsed);
        }
    }
}
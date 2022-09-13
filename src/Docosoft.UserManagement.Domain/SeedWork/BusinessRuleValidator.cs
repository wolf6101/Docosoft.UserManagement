namespace Docosoft.UserManagement.Domain.SeedWork
{
    public class BusinessRuleValidator
    {
        private readonly IList<IBusinessRule> _rules;

        public BusinessRuleValidator(IList<IBusinessRule> rules)
        {
            _rules = rules;
        }

        public async Task AssertRules() {
            foreach (var rule in _rules) {
                await CheckRule(rule);
            }
        }

        protected async Task CheckRule(IBusinessRule rule)
        {
            var isBroken = await rule.IsBroken();
            if (isBroken)
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
namespace Docosoft.UserManagement.Domain.SeedWork
{
    public interface IBusinessRuleValidator
    {
        Task AssertRules(IEnumerable<IBusinessRule> rules);
    }
}
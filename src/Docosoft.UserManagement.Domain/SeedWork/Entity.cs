namespace Docosoft.UserManagement.Domain.SeedWork
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        
        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
namespace Docosoft.UserManagement.Domain.SeedWork
{
    public abstract class Entity
    {
        protected Entity(Guid id) => Id = id;

        public Guid Id { get; private set; }
        
        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
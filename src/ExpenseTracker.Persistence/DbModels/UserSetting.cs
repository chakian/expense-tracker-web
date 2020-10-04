using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Persistence.DbModels
{
    public class UserSetting : BaseAuditableDbo
    {
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        public int DefaultBudgetId { get; set; }
        public virtual Budget DefaultBudget { get; set; }
    }
}

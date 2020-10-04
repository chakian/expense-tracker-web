using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ExpenseTracker.Persistence.DbModels
{
    public class BudgetUser : BaseAuditableDbo
    {
        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        public int Role { get; set; }
    }
}

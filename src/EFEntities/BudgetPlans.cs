using EFEntities.Base;
using System.Collections.Generic;

namespace EFEntities
{
    public partial class BudgetPlans : AuditableEntity
    {
        public BudgetPlans()
        {
            BudgetPlanCategories = new HashSet<BudgetPlanCategories>();
        }

        public int BudgetPlanId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int BudgetId { get; set; }

        public virtual Budgets Budget { get; set; }
        public virtual ICollection<BudgetPlanCategories> BudgetPlanCategories { get; set; }
    }
}

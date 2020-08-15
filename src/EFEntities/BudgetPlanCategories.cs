using EFEntities.Base;

namespace EFEntities
{
    public partial class BudgetPlanCategories : AuditableEntity
    {
        public int BudgetPlanCategoryId { get; set; }
        public decimal PlannedAmount { get; set; }
        public int BudgetPlanId { get; set; }
        public int CategoryId { get; set; }

        public virtual BudgetPlans BudgetPlan { get; set; }
        public virtual Categories Category { get; set; }
    }
}

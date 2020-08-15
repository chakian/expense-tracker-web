using System;

namespace EFEntities
{
    public partial class BudgetPlanCategories
    {
        public int BudgetPlanCategoryId { get; set; }
        public bool IsActive { get; set; }
        public string InsertUserId { get; set; }
        public DateTime InsertTime { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public decimal PlannedAmount { get; set; }
        public int BudgetPlanId { get; set; }
        public int CategoryId { get; set; }

        public virtual BudgetPlans BudgetPlan { get; set; }
        public virtual Categories Category { get; set; }
        public virtual Users InsertUser { get; set; }
        public virtual Users UpdateUser { get; set; }
    }
}

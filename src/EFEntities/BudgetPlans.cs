using System;
using System.Collections.Generic;

namespace EFEntities
{
    public partial class BudgetPlans
    {
        public BudgetPlans()
        {
            BudgetPlanCategories = new HashSet<BudgetPlanCategories>();
        }

        public int BudgetPlanId { get; set; }
        public bool IsActive { get; set; }
        public string InsertUserId { get; set; }
        public DateTime InsertTime { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int BudgetId { get; set; }

        public virtual Budgets Budget { get; set; }
        public virtual Users InsertUser { get; set; }
        public virtual Users UpdateUser { get; set; }
        public virtual ICollection<BudgetPlanCategories> BudgetPlanCategories { get; set; }
    }
}

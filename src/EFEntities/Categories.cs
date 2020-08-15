using System;
using System.Collections.Generic;

namespace EFEntities
{
    public partial class Categories
    {
        public Categories()
        {
            BudgetPlanCategories = new HashSet<BudgetPlanCategories>();
            InverseParentCategory = new HashSet<Categories>();
            TransactionItem = new HashSet<TransactionItem>();
            TransactionTemplates = new HashSet<TransactionTemplates>();
            Transactions = new HashSet<Transactions>();
        }

        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public string InsertUserId { get; set; }
        public DateTime InsertTime { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Name { get; set; }
        public bool IsIncomeCategory { get; set; }
        public int? ParentCategoryId { get; set; }
        public int Order { get; set; }
        public int BudgetId { get; set; }

        public virtual Budgets Budget { get; set; }
        public virtual Users InsertUser { get; set; }
        public virtual Categories ParentCategory { get; set; }
        public virtual Users UpdateUser { get; set; }
        public virtual ICollection<BudgetPlanCategories> BudgetPlanCategories { get; set; }
        public virtual ICollection<Categories> InverseParentCategory { get; set; }
        public virtual ICollection<TransactionItem> TransactionItem { get; set; }
        public virtual ICollection<TransactionTemplates> TransactionTemplates { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace EFEntities
{
    public partial class Budgets
    {
        public Budgets()
        {
            Accounts = new HashSet<Accounts>();
            BudgetPlans = new HashSet<BudgetPlans>();
            BudgetUsers = new HashSet<BudgetUsers>();
            Categories = new HashSet<Categories>();
            TransactionTemplates = new HashSet<TransactionTemplates>();
        }

        public int BudgetId { get; set; }
        public bool IsActive { get; set; }
        public string InsertUserId { get; set; }
        public DateTime InsertTime { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Name { get; set; }
        public int CurrencyId { get; set; }

        public virtual Currencies Currency { get; set; }
        public virtual Users InsertUser { get; set; }
        public virtual Users UpdateUser { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
        public virtual ICollection<BudgetPlans> BudgetPlans { get; set; }
        public virtual ICollection<BudgetUsers> BudgetUsers { get; set; }
        public virtual ICollection<Categories> Categories { get; set; }
        public virtual ICollection<TransactionTemplates> TransactionTemplates { get; set; }
    }
}

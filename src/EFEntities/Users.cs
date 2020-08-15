using EFEntities.Base;
using System.Collections.Generic;

namespace EFEntities
{
    public partial class Users : AuditableUserEntity
    {
        public Users()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();

            AccountsInsertUser = new HashSet<Accounts>();
            AccountsUpdateUser = new HashSet<Accounts>();
            BudgetPlanCategoriesInsertUser = new HashSet<BudgetPlanCategories>();
            BudgetPlanCategoriesUpdateUser = new HashSet<BudgetPlanCategories>();
            BudgetPlansInsertUser = new HashSet<BudgetPlans>();
            BudgetPlansUpdateUser = new HashSet<BudgetPlans>();
            BudgetUsersInsertUser = new HashSet<BudgetUsers>();
            BudgetUsersUpdateUser = new HashSet<BudgetUsers>();
            BudgetUsersUser = new HashSet<BudgetUsers>();
            BudgetsInsertUser = new HashSet<Budgets>();
            BudgetsUpdateUser = new HashSet<Budgets>();
            CategoriesInsertUser = new HashSet<Categories>();
            CategoriesUpdateUser = new HashSet<Categories>();
            TransactionTemplatesInsertUser = new HashSet<TransactionTemplates>();
            TransactionTemplatesUpdateUser = new HashSet<TransactionTemplates>();
            TransactionTemplatesUser = new HashSet<TransactionTemplates>();
            TransactionsInsertUser = new HashSet<Transactions>();
            TransactionsUpdateUser = new HashSet<Transactions>();
        }

        #region Custom columns
        public int? ActiveBudgetId { get; set; }
        #endregion

        #region Foreign Keys
        public virtual ICollection<Accounts> AccountsInsertUser { get; set; }
        public virtual ICollection<Accounts> AccountsUpdateUser { get; set; }
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual ICollection<BudgetPlanCategories> BudgetPlanCategoriesInsertUser { get; set; }
        public virtual ICollection<BudgetPlanCategories> BudgetPlanCategoriesUpdateUser { get; set; }
        public virtual ICollection<BudgetPlans> BudgetPlansInsertUser { get; set; }
        public virtual ICollection<BudgetPlans> BudgetPlansUpdateUser { get; set; }
        public virtual ICollection<BudgetUsers> BudgetUsersInsertUser { get; set; }
        public virtual ICollection<BudgetUsers> BudgetUsersUpdateUser { get; set; }
        public virtual ICollection<BudgetUsers> BudgetUsersUser { get; set; }
        public virtual ICollection<Budgets> BudgetsInsertUser { get; set; }
        public virtual ICollection<Budgets> BudgetsUpdateUser { get; set; }
        public virtual ICollection<Categories> CategoriesInsertUser { get; set; }
        public virtual ICollection<Categories> CategoriesUpdateUser { get; set; }
        public virtual ICollection<TransactionTemplates> TransactionTemplatesInsertUser { get; set; }
        public virtual ICollection<TransactionTemplates> TransactionTemplatesUpdateUser { get; set; }
        public virtual ICollection<TransactionTemplates> TransactionTemplatesUser { get; set; }
        public virtual ICollection<Transactions> TransactionsInsertUser { get; set; }
        public virtual ICollection<Transactions> TransactionsUpdateUser { get; set; }
        #endregion
    }
}

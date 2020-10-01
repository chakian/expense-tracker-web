using EFEntities.Base;
using System.Collections.Generic;

namespace EFEntities
{
    public partial class Accounts : AuditableEntity
    {
        public Accounts()
        {
            TransactionTemplatesSourceAccount = new HashSet<TransactionTemplates>();
            TransactionTemplatesTargetAccount = new HashSet<TransactionTemplates>();
            TransactionsSourceAccount = new HashSet<Transactions>();
            TransactionsTargetAccount = new HashSet<Transactions>();
        }

        public int AccountId { get; set; }
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public int AccountTypeId { get; set; }
        public int BudgetId { get; set; }

        public virtual AccountTypes AccountType { get; set; }
        public virtual Budgets Budget { get; set; }
        public virtual ICollection<TransactionTemplates> TransactionTemplatesSourceAccount { get; set; }
        public virtual ICollection<TransactionTemplates> TransactionTemplatesTargetAccount { get; set; }
        public virtual ICollection<Transactions> TransactionsSourceAccount { get; set; }
        public virtual ICollection<Transactions> TransactionsTargetAccount { get; set; }
    }
}

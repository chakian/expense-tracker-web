using EFEntities.Base;
using System;
using System.Collections.Generic;

namespace EFEntities
{
    public partial class Transactions : AuditableEntity
    {
        public Transactions()
        {
            TransactionItem = new HashSet<TransactionItem>();
        }

        public int TransactionId { get; set; }
        
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int SourceAccountId { get; set; }
        public int? TargetAccountId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Accounts SourceAccount { get; set; }
        public virtual Accounts TargetAccount { get; set; }
        public virtual ICollection<TransactionItem> TransactionItem { get; set; }
    }
}

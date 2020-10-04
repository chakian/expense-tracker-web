using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Persistence.DbModels
{
    public class Transaction : BaseAuditableDbo
    {
        [Required]
        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int? TargetAccountId { get; set; }
        public Account TargetAccount { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSplitTransaction { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}

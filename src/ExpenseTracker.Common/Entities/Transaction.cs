using System;

namespace ExpenseTracker.Common.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public DateTime Date { get; set; }
        
        public int AccountId { get; set; }
        public string AccountName { get; set; }

        public int? TargetAccountId { get; set; }
        public string TargetAccountName { get; set; }

        public bool IsSplitTransaction { get; set; }
        
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}

using System;

namespace ExpenseTracker.Common.Contracts.Command
{
    public class CreateTransactionRequest : BaseBudgetRelatedRequest
    {
        public DateTime Date { get; set; }
        public int AccountId { get; set; }
        public int? CategoryId { get; set; }
        public int? TargetAccountId { get; set; }
        public bool IsSplitTransaction { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
        public string Description { get; set; }
    }
}

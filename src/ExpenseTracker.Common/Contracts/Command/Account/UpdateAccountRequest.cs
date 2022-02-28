namespace ExpenseTracker.Common.Contracts.Command
{
    public class UpdateAccountRequest : BaseBudgetRelatedRequest
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal AccountBalance { get; set; }
    }
}

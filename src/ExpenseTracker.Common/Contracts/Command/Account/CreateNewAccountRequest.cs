namespace ExpenseTracker.Common.Contracts.Command
{
    public class CreateNewAccountRequest : BaseBudgetRelatedRequest
    {
        public string AccountName { get; set; }
        public int AccountType { get; set; }
        public decimal AccountBalance { get; set; }
    }
}